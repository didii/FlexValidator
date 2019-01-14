namespace Validator.Tests.Models {
    class SomeModel {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool[] Bits { get; set; }
        public SubModel Sub { get; set; }
        public DoubleModel DoubleLeft { get; set; }
        public DoubleModel DoubleRight { get; set; }
    }
}