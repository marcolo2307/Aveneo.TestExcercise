using System;

namespace Aveneo.TestExcercise.ApplicationCore.Entities
{
    public class DataObjectGallery : EntityBase
    {
        public int DataObjectId { get; set; }
        public int Sequence { get; set; }
        public Guid FileName { get; set; }
    }
}
