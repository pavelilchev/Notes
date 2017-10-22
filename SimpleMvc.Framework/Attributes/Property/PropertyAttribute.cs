namespace SimpleMvc.Framework.Attributes.Property
{
    using System.ComponentModel.DataAnnotations;

    public abstract class PropertyAttribute : ValidationAttribute
    {
        public override abstract bool IsValid(object value);
    }
}
