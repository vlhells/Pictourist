namespace Pictourist.Areas.Admin.Models
{
    public class Friend
    {
        public int Id { get; set; }
        public string FirstFriendId { get; set; }
        public string SecondFriendId { get; set; }
        public byte RelationType { get; set; } // 1 -- первый отправил заявку второму, -1 -- второй, 0 -- друзья.
    }
}
