using System;

namespace FlexValidator.Example.App.Models {
    class SubModel : BaseModel {
        public string Name { get; set; }
        public SubModelType Type { get; set; }
        public DateTime? DateTime { get; set; }
    }
}