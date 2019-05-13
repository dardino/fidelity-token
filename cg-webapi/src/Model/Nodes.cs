namespace cg_webapi.Model {
    public class Nodes {
        public Node eth { get; set; }
        public Node bootstrap { get; set; }
	}

	public class Node
	{
        public string Ip { get; set; }
    }
}