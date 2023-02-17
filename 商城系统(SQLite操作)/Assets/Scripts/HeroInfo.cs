namespace DefaultNamespace {
    public class HeroInfo {
        public string Name { get; set; }
        public int Money { get; set; }
        public int HP { get; set; }
        public int AD { get; set; }
        public int Defense { get; set; }
        public int Knowing { get; set; }

        public string[] Equips { get; set; } = new string[8];
    }
}