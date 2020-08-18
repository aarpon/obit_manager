using System;
using obit_manager_settings.components;
using System.Collections.Generic;
using NLog;
using NLog.Config;
using obit_manager_settings.components.io;
using static obit_manager_settings.Constants;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace obit_manager_settings
{
    /// <summary>
    /// Manager of all oBIS settings.
    /// </summary>
    public class SettingsManager
    {
        // Logger
        private static Logger sLogger = LogManager.GetCurrentClassLogger();

        // Settings parsers
        private readonly ManagerSettingsParser mManagerParser;
        private readonly AnnotationToolSettingsParser mAnnotationToolParser;
        private readonly DatamoverJSLSettingsParser mDatamoverJSLParser;
        private readonly DatamoverSettingsParser mDatamoverParser;

        // List of instances
        private List<Instance> mInstances;

        // List of Client, Datamover and Server objects
        private List<Client> mClients;
        private List<Datamover> mDatamovers;
        private List<Server> mServers;

        // Keep track of the selected instance
        private int mSelectedInstanceIndex = 0;

 
        public SettingsManager()
        {
            // Initialize application state
            this.ApplicationState = State.OBIT_NOT_INSTALLED;

            // Initialize all lists
            this.mInstances = new List<Instance>();
            this.mClients = new List<Client>();
            this.mDatamovers = new List<Datamover>();
            this.mServers = new List<Server>();

            // Load (if possible, otherwise instantiate with default values)
            // the oBIT Manager Application Settings
            this.mManagerParser = new ManagerSettingsParser();

            // Load (if possible, otherwise instantiate with default values)
            // the AnnotationTool Settings
            this.mAnnotationToolParser = new AnnotationToolSettingsParser();

            // Load (if possible, otherwise instantiate with default values)
            // the Datamover JSL Settings
            this.mDatamoverJSLParser = new DatamoverJSLSettingsParser(
                this.mManagerParser.InstallationDir,
                this.mManagerParser.DatamoverRelativeDirList
            );

            // Load (if possible, otherwise instantiate with default values)
            // the Datamover Settings
            this.mDatamoverParser = new DatamoverSettingsParser(
                this.mManagerParser.InstallationDir,
                this.mManagerParser.DatamoverRelativeDirList
            );

            // If needed, update the relative DatamoverJSL directories
            this.mManagerParser.DatamoverRelativeDirList = this.mDatamoverJSLParser.GetRelativeDatamoverJSLDirs();

            // Populate the instances and update the state
            this.ApplicationState = this.PopulateInstances();

            // Set the first instance to be the selected one
            this.mSelectedInstanceIndex = 0;
        }

        public State ApplicationState { get; private set; }

        public void Reload()
        {
            // @Todo Re-implement correctly!
            this.mManagerParser.Load();
            this.mAnnotationToolParser.Load();
            this.mDatamoverJSLParser.Load();
            this.mDatamoverParser.Load();

            // Populate the instances
            this.PopulateInstances();
        }

        public void Save()
        {
            // @Todo Implement!
            this.mManagerParser.Save();
            this.mAnnotationToolParser.Save(this);
            this.mDatamoverJSLParser.Save();
            this.mDatamoverParser.Save();
        }

        private State PopulateInstances()
        {
            // Get the list of instances from the Annotation Tool parser
            var configurations = this.mAnnotationToolParser.Configurations;

            int currentInstance = 0;

            // Inform
            foreach (KeyValuePair<string, Dictionary<string, string>> configuration in configurations)
            {
                // Inform
                sLogger.Info("Processing instance '" + configuration.Key + "'.");

                // Create a new Client from the AnnotationTool settings
                Client client = new Client(configuration.Value);

                // Create a Datamover object from the Datamover and DatamoverJSL configurations
                Datamover datamover;
                try
                {
                    datamover = new Datamover(client.DatamoverIncomingDir, this.mDatamoverJSLParser,
                        this.mDatamoverParser);
                }
                catch (ConfigurationException)
                {
                    // Inform
                    sLogger.Error("Configuration '" + configuration.Key + "' is invalid and will be skipped.");

                    // Skip this configuration.
                    continue;
                }

                // Create a Server object
                Server server;
                try
                {
                    server = new Server(client, this.mDatamoverParser);
                }
                catch (ConfigurationException e)
                {
                    // Inform
                    sLogger.Error("Configuration '" + configuration.Key + "' is invalid and will be skipped.");

                    // Skip this configuration.
                    continue;
                }

                // Add the objects
                this.mClients.Add(client);
                this.mDatamovers.Add(datamover);
                this.mServers.Add(server);

                // Create new instance
                Instance instance = new Instance(currentInstance, currentInstance, currentInstance);

                // Add to the instance list
                this.mInstances.Add(instance);

                // Update the counter
                currentInstance++;

                // Inform
                sLogger.Info("Created instance '" + configuration.Key + "'.");
            }

            // @TODO Set the proper State
            return State.OBIT_NOT_INSTALLED;
        }

        /// <summary>
        /// Return a list of Instance names.
        /// </summary>
        /// <returns>List of Instance names.</returns>
        public List<string> GetInstanceNames()
        {
            List<string> names = new List<string>();

            foreach (Instance instance in this.mInstances)
            {
                names.Add(this.mClients[instance.ClientIndex].ConfigurationName);
            }

            // Return the list of names
            return names;
        }

        /// <summary>
        /// Return the Instance with given configuration name.
        /// </summary>
        /// <param name="name">Configuration name.</param>
        /// <returns>Instance or null if not found.</returns>
        public Instance GetInstanceByName(string name)
        {
            // Look for the instance with matching configuration name
            foreach (Instance instance in this.mInstances)
            {
                if (this.mClients[instance.ClientIndex].ConfigurationName.Equals(name))
                {
                    return instance;
                }
            }

            // If not found, return null
            return null;
        }

        /// <summary>
        /// Returns Client associated to Instance with given index.
        /// </summary>
        /// <param name="index">Index of the Instance.</param>
        /// <returns>Client object.</returns>
        public Client GetClientFromInstanceWithIndex(int index)
        {
            return this.mClients[this.mInstances[index].ClientIndex];
        }

        /// <summary>
        /// Returns Client associated with currently selected Instance.
        /// </summary>
        /// <returns>Client object.</returns>
        public Client GetClientFromSelectedInstance()
        {
            return this.mClients[this.SelectedInstance.ClientIndex];
        }

        /// <summary>
        /// Returns Client associated to given Instance.
        /// </summary>
        /// <param name="instance">Instance.</param>
        /// <returns>Client object.</returns>
        public Client GetClientFromInstance(Instance instance)
        {
            return this.mClients[instance.ClientIndex];
        }

        /// <summary>
        /// Returns Datamover associated to Instance with given index.
        /// </summary>
        /// <param name="index">Index of the Instance.</param>
        /// <returns>Datamover object.</returns>
        public Datamover GetDatamoverFromInstanceWithIndex(int index)
        {
            return this.mDatamovers[this.mInstances[index].DatamoverIndex];
        }

        /// <summary>
        /// Returns Datamover associated to given Instance.
        /// </summary>
        /// <param name="instance">Instance.</param>
        /// <returns>Datamover object.</returns>
        public Datamover GetDatamoverFromInstance(Instance instance)
        {
            return this.mDatamovers[instance.DatamoverIndex];
        }

        /// <summary>
        /// Returns Datamover associated with currently selected Instance.
        /// </summary>
        /// <returns>Datamover object.</returns>
        public Datamover GetDatamoverFromSelectedInstance()
        {
            return this.mDatamovers[this.SelectedInstance.DatamoverIndex];
        }

        /// <summary>
        /// Returns Server associated to Instance with given index.
        /// </summary>
        /// <param name="index">Index of the Instance.</param>
        /// <returns>Server object.</returns>
        public Server GetServerFromInstanceWithIndex(int index)
        {
            return this.mServers[this.mInstances[index].ServerIndex];
        }

        /// <summary>
        /// Returns Server associated with currently selected Instance.
        /// </summary>
        /// <returns>Server object.</returns>
        public Server GetServerFromSelectedInstance()
        {
            return this.mServers[this.SelectedInstance.ServerIndex];
        }

        /// <summary>
        /// Returns Server associated to given Instance.
        /// </summary>
        /// <param name="instance">Instance.</param>
        /// <returns>Server object.</returns>
        public Server GetServerFromInstance(Instance instance)
        {
            return this.mServers[instance.ServerIndex];
        }

        /// <summary>
        /// Return the Client with given user data dir.
        /// </summary>
        /// <param name="userDataDir">User data directory.</param>
        /// <returns>Client or null if not found.</returns>
        public Client GetClientByUserDataDir(string userDataDir)
        {
            // Look for the Client with matching user data dir
            foreach (Instance instance in this.mInstances)
            {
                if (this.mClients[instance.ClientIndex].UserDataDir.Equals(userDataDir))
                {
                    return this.mClients[instance.ClientIndex];
                }
            }

            // If not found, return null
            return null;
        }


        /// <summary>
        /// Return the Datamover with given user incoming dir.
        /// </summary>
        /// <param name="incomingDir">Incoming directory.</param>
        /// <returns>Datamover or null if not found.</returns>
        public Datamover GetDatamoverByIncomingDir(string incomingDir)
        {
            // Look for the Datamover with matching incoming dir
            foreach (Instance instance in this.mInstances)
            {
                if (this.mDatamovers[instance.DatamoverIndex].IncomingTarget.Equals(incomingDir))
                {
                    return this.mDatamovers[instance.DatamoverIndex];
                }
            }

            // If not found, return null
            return null;
        }

        /// <summary>
        /// Return the Server with given label.
        /// </summary>
        /// <param name="label">Server label.</param>
        /// <returns>Server or null if not found.</returns>
        public Server GetServerByLabel(string label)
        {
            // Look for the Server with matching label
            foreach (Instance instance in this.mInstances)
            {
                if (this.mServers[instance.ServerIndex].Label.Equals(label))
                {
                    return this.mServers[instance.ServerIndex];
                }
            }

            // If not found, return null
            return null;
        }

        /// <summary>
        /// Select the Instance with given configuration name.
        /// </summary>
        /// <param name="name">Configuration name.</param>
        public void SetSelectedInstanceByName(string name)
        {
            // Find the Instance by name

            int index = -1;
            for (int i = 0; i < this.NumInstances; i++)
            {
                if (this.mClients[this.mInstances[i].ClientIndex].ConfigurationName.Equals(name))
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                throw new Exception("No Instance with given name found!");
            }
            this.SelectedInstanceIndex = index;
        }

        /// <summary>
        /// Replace the Client for given Instance with a new one.
        /// </summary>
        /// <param name="instance">Instance object.</param>
        /// <param name="updatedClient">Client object.</param>
        public void ReplaceClientObjectForInstance(Instance instance, Client updatedClient)
        {
            this.mClients[instance.ClientIndex] = updatedClient;
        }

        /// <summary>
        /// Change the Client index for given Instance.
        /// </summary>
        /// <param name="instance">Instance object.</param>
        /// <param name="newClientIndex">New Client index.</param>
        public void ChangeClientIndexForInstance(Instance instance, int newClientIndex)
        {
            if (newClientIndex < 0 || newClientIndex > (this.NumInstances - 1))
            {
                throw new ArgumentOutOfRangeException("newClientIndex", "Must be between 0 and " + (this.NumInstances - 1));
            }
            instance.ClientIndex = newClientIndex;
        }

        /// <summary>
        /// Change the Client index for current Instance.
        /// </summary>
        /// <param name="newClientIndex">New Client index.</param>
        public void ChangeClientIndexForCurrentInstance(int newClientIndex)
        {
            this.ChangeClientIndexForInstance(this.SelectedInstance, newClientIndex);
        }

        /// <summary>
        /// Replace the Datamover for given Instance with a new one.
        /// </summary>
        /// <param name="instance">Instance object.</param>
        /// <param name="updatedDatamover">Datamover object.</param>
        public void ReplaceDatamoverObjectForInstance(Instance instance, Datamover updatedDatamover)
        {
            this.mDatamovers[instance.DatamoverIndex] = updatedDatamover;
        }

        /// <summary>
        /// Change the Datamover index for given Instance.
        /// </summary>
        /// <param name="instance">Instance object.</param>
        /// <param name="newDatamoverIndex">New Datamover index.</param>
        public void ChangeDatamoverIndexForInstance(Instance instance, int newDatamoverIndex)
        {
            if (newDatamoverIndex < 0 || newDatamoverIndex > (this.NumInstances - 1))
            {
                throw new ArgumentOutOfRangeException("newDatamoverIndex", "Must be between 0 and " + (this.NumInstances - 1));
            }
            instance.DatamoverIndex = newDatamoverIndex;
        }

        /// <summary>
        /// Change the Datamover index for current Instance.
        /// </summary>
        /// <param name="newDatamoverIndex">New Datamover index.</param>
        public void ChangeDatamoverIndexForCurrentInstance(int newDatamoverIndex)
        {
            this.ChangeDatamoverIndexForInstance(this.SelectedInstance, newDatamoverIndex);
        }

        /// <summary>
        /// Replace the Server for given Instance with a new one.
        /// </summary>
        /// <param name="instance">Instance object.</param>
        /// <param name="updatedServer">Server object.</param>
        public void ReplaceServerObjectForInstance(Instance instance, Server updatedServer)
        {
            this.mServers[instance.ServerIndex] = updatedServer;
        }

        /// <summary>
        /// Change the Server index for given Instance.
        /// </summary>
        /// <param name="instance">Instance object.</param>
        /// <param name="newServerIndex">New Server index.</param>
        public void ChangeServerIndexForInstance(Instance instance, int newServerIndex)
        {
            if (newServerIndex < 0 || newServerIndex > (this.NumInstances - 1))
            {
                throw new ArgumentOutOfRangeException("newServerIndex", "Must be between 0 and " + (this.NumInstances - 1));
            }
            instance.ServerIndex = newServerIndex;
        }

        /// <summary>
        /// Change the Server index for current Instance.
        /// </summary>
        /// <param name="newServerIndex">New Server index.</param>
        public void ChangeServerIndexForCurrentInstance(int newServerIndex)
        {
            this.ChangeServerIndexForInstance(this.SelectedInstance, newServerIndex);
        }

        /// <summary>
        /// Swap two Intances in the list of Instances.
        /// </summary>
        /// <param name="index1">Index of the first Instance to swap.</param>
        /// <param name="index2">Index of the second Instance to swap.</param>
        public void SwapInstances(int index1, int index2)
        {
            if (index1 >= this.mInstances.Count || index2 >= this.mInstances.Count)
            {
                throw new ArgumentOutOfRangeException(
                    "Max possible Instance index is " + 
                    (this.mInstances.Count - 1) + 
                    ".");
            }

            Instance tmp = this.mInstances[index1];
            this.mInstances[index1] = this.mInstances[index2];
            this.mInstances[index2] = tmp;
        }

        #region properties

        // openBIS Importer Toolset installation dir
        public string InstallationDir
        {
            get => this.mManagerParser.InstallationDir;
            set
            {
                if (this.mManagerParser != null)
                {
                    this.mManagerParser.InstallationDir = value;
                }
            }
        }

        /// <summary>
        /// Index of the selected Instance
        /// </summary>
        public int SelectedInstanceIndex
        {
            get => this.mSelectedInstanceIndex;
            set
            {
                if (value >= 0 && value < mInstances.Count)
                {
                    this.mSelectedInstanceIndex = value;
                }
                else
                {
                    throw new Exception("There is no Instance with index " + value.ToString() + ".");
                }
            }
        }

        /// <summary>
        /// Selected Instance
        /// </summary>
        public Instance SelectedInstance
        {
            get
            {
                if (this.mSelectedInstanceIndex >= 0 && this.mSelectedInstanceIndex < this.mInstances.Count)
                {
                    return this.mInstances[this.mSelectedInstanceIndex];
                }
                else
                {
                    throw new Exception("There is no valid selected Instance.");
                }
            }

            set
            {
                int index = this.mInstances.FindIndex(a => a == value);
                if (index == -1)
                {
                    throw new Exception("The passed Instance is not recognized!");
                }
                this.SelectedInstanceIndex = index;
            }
        }

        /// <summary>
        /// Return the number of instances.
        /// </summary>
        public int NumInstances
        {
            get => this.mInstances.Count; 
        }

        public int NumClients
        {
            get => this.mInstances.Count;
        }

        public int NumDatamovers
        {
            get => this.mInstances.Count;
        }

        public int numServers
        {
            get => this.mInstances.Count;
        }

        public List<string> ClientStrings
        {
            get
            {
                List<string> clientStrings = new List<string>(this.NumClients);
                foreach (Instance instance in this.mInstances)
                {
                    clientStrings.Add(this.mClients[instance.ClientIndex].UserDataDir);
                }
                return clientStrings;
            }
        }

        public List<string> DatamoverStrings
        {
            get
            {
                List<string> datamoverStrings = new List<string>(this.NumDatamovers);
                foreach (Instance instance in this.mInstances)
                {
                    datamoverStrings.Add(this.mDatamovers[instance.DatamoverIndex].IncomingTarget);
                }
                return datamoverStrings;
            }
        }

        public List<string> ServerStrings
        {
            get
            {
                List<string> serverStrings = new List<string>(this.numServers);
                foreach (Instance instance in this.mInstances)
                {
                    serverStrings.Add(this.mServers[instance.ServerIndex].Label);
                }
                return serverStrings;
            }
        }

        #endregion properties
    }
}
