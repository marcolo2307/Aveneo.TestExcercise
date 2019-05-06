﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aveneo.TestExcercise.ApplicationCore.Entities
{
    public class Feature
        : AutoIncEntityBase
    {
        public string IconName { get; set; }
    }
}
