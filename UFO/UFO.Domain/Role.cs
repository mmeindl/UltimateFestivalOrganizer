﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFO.Domain
{
    [Serializable]
    public class Role
    {
        public Role()
        {
        }

        public Role(int id, string title)
        {
            this.Id = id;
            this.Title = title;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
