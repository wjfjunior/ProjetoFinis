using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace Finis.Models
{
    public abstract class EntidadeAbstrata
    {
        public EntidadeAbstrata()
        {
            this.id = 0;
            this.date_insert = DateTime.Now;
            this.date_update = DateTime.Now;
            this.user_insert = "";
            this.user_update = "";
        }

        [Key]
        [Editable(false)]
        public int id { get; set; }

        private string _user_insert;
        private string _user_update;

        private DateTime _date_insert;
        private DateTime _date_update;

        public virtual string user_insert
        {
            get { return _user_insert; }
            set { _user_insert = value; }
        }

        public virtual string user_update
        {
            get { return _user_update; }
            set { _user_update = value; }
        }

        public virtual DateTime date_insert
        {
            get { return _date_insert; }
            set { _date_insert = value; }
        }

        public virtual DateTime date_update
        {
            get { return _date_update; }
            set { _date_update = value; }
        }

        public virtual void ConfigurarParaSalvar()
        {
            if (HttpContext.Current != null)
            {
                string _Login = HttpContext.Current.User.Identity.Name;

                if (!string.IsNullOrEmpty(_Login))
                {
                    if (_Login.Length > 20)
                        _Login = _Login.Substring(0, 20);
                }

                if (this.id == 0)
                {
                    this.date_insert = DateTime.Now;
                    this.date_update = DateTime.Now;
                    this.user_insert = _Login;
                    this.user_update = "";
                }
                else
                {
                    this.date_update = DateTime.Now;
                    this.user_update = _Login;
                }
            }
            else
            {
                this.user_insert = "DEBUG";
                this.user_update = "DEBUG";
            }
        }

        public string Serializar()
        {
            return JsonConvert.SerializeObject(this);
        }

        private string SerializeJson()
        {
            using (MemoryStream buffer = new MemoryStream())
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(this.GetType());
                ser.WriteObject(buffer, this);
                return ASCIIEncoding.ASCII.GetString(buffer.ToArray());
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}