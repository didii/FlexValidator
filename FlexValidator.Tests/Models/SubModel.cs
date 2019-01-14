using System;

namespace FlexValidator.Tests.Models {
    class SubModel {
        public long Id { get; set; }
        public string Name { get; set; }
        public SubModelType Type { get; set; }
        public DateTime? DateTime { get; set; }
    }
}