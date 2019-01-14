namespace Validator.Example.App.Models {
    class SomeModel : BaseModel {
        public string Name { get; set; }
        public bool[] Bits { get; set; }
        public SubModel Sub { get; set; }
        public DoubleModel DoubleLeft { get; set; }
        public DoubleModel DoubleRight { get; set; }
    }
}