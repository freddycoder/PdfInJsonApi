namespace PdfInJsonApi
{
    internal class PaginationHeaders
    {
        public int Total {get;set;}
        public int Page {get;set;}
        public int PageSize {get;set;}

        public PaginationHeaders(int totalCount, int page, int pageSize)
        {
            this.Total = totalCount;
            this.Page = page;
            this.PageSize = pageSize;
        }
    }
}