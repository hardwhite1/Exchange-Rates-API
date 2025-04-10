
namespace TestMvc1.Services
{
    public class Pagination
    {
        //Declare propeties to basic inputs
        public int TotalItems {get; set;}

        public int PageSize {get; set;}

        public int CurrentPage {get; set;}

        //Declare properties to the output

        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        public int StartIndex => (CurrentPage - 1)* PageSize;

        //Declare a public constructor
        public Pagination(int totalItems, int pageSize, int currentPage)
        {
            TotalItems = totalItems;
            PageSize = pageSize;
            CurrentPage = currentPage < 1 ? 1 : currentPage;
            
        }

        //Then create an empty constructor, yet to find out why
        public Pagination()
        {

        }

        public bool HasNextPage => CurrentPage < TotalPages;

        public bool HasPreviousPage => CurrentPage > 1;
    }
}