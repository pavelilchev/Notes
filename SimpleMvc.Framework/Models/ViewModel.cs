namespace SimpleMvc.Framework.Models
{
    using System.Collections.Generic;

    public class ViewModel
    {
        public ViewModel()
        {
            this.Data = new Dictionary<string, string>();
            this.Errors = new List<string>();
        }

        public IDictionary<string, string> Data { get; }

        public List<string> Errors { get; set; }

        public string this[string key]
        {
            get => this.Data[key];
            set => this.Data[key] = value;
        }
    }
}
