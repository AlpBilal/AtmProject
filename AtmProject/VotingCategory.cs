namespace AtmProject
{
    internal class VotingCategory{    
        public String category {get; set;}
        public int voteCount {get; set;} = 0;

        public VotingCategory(String category)
        {
            this.category = category;
    
        }
    }
}