using ShopBooks.DataAccess.Repository.IRepository;
using ShopBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBooks.DataAccess.Repository
{
    public class OrderDetailRepository:Repository<OrderDetail>, IOrderDetail
    {
    public readonly ApplicationDbContext _db;

    public OrderDetailRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(OrderDetail obj)
    {
        _db.OrderDetails.Update(obj);
    }
}
}
