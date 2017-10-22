namespace SimpleMvc.Framework.Attributes.Property
{
    using System.Text.RegularExpressions;

    public class RegexAttribute : PropertyAttribute
    {
        private const string defaultErrorMessage = "{0} is not valid";

        private readonly string pattern;

        public RegexAttribute(string pattern)
        {
            this.pattern = "^" + pattern + "$";           
        }

        public override bool IsValid(object value)
        {
            return Regex.IsMatch(value.ToString(), this.pattern);
        }
    }
}
