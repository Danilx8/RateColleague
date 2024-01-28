namespace RateColleague.Models
{
    public class Pagination
    {
        public int PageNum { get; set; }
        public int Limit { get; set; }
        public Pagination()
        {
            PageNum = 1;
            Limit = 10;
        }
        public Pagination(int pageNumber, int pageSize)
        {
            PageNum = pageNumber < 1 ? 1 : pageNumber;
            Limit = pageSize < 1 ? 1 : pageSize;
        }

    }
}
