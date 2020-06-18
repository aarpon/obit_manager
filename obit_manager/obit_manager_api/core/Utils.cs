namespace obit_manager_api.core
{
    public static class Utils
    {
        /// <summary>
        /// Boolean to string representation.
        /// </summary>
        /// <param name="value">Boolean value.</param>
        /// <returns>String representation of the boolean.</returns>
        public static string BoolToString(bool value)
        {
            if (value)
            {
                return "true";
            }

            return "false";
        }

        /// <summary>
        /// String to boolean representation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool StringToBool(string value)
        {
            if (value.ToLowerInvariant() == "true")
            {
                return true;
            }

            return false;
        }
    }
}