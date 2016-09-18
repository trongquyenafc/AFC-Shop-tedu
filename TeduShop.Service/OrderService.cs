using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Model.Models;
using TedShop.Data.Repositories;
using TedShop.Data.Infrastructure;

namespace TeduShop.Service
{

    public interface IOrderService
    {
        bool Create(Order order, List<OrderDetail> orderDetails);
        void Update(Order order);

        IEnumerable<Order> GetAll(string keyword);
        Order GetById(int id);
        Order Delete(int id);
        void Save();
    }
    public class OrderService : IOrderService
    {
        IOrderRepository _orderRepository;
        IOrderDetailRepository _orderDetailRepository;
        IUnitOfWork _unitOfWork;
        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IUnitOfWork unitOfWork)
        {
            this._orderRepository = orderRepository;
            this._orderDetailRepository = orderDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        public bool Create(Order order, List<OrderDetail> orderDetails)
        {
            try
            {
                _orderRepository.Add(order);
                _unitOfWork.Commit();
                foreach (var orderDetail in orderDetails)
                {
                    orderDetail.OrderID = order.ID;
                    _orderDetailRepository.Add(orderDetail);
                }
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Update(Order order)
        {
            _orderRepository.Update(order);
        }

        public IEnumerable<Order> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _orderRepository.GetMulti(x => x.CustomerName.Contains(keyword) || x.CustomerMobile.Contains(keyword) || x.CustomerEmail.Contains(keyword));

            }
            else
            {
                return _orderRepository.GetAll();
            }
        }

        public Order GetById(int id)
        {
            return _orderRepository.GetSingleById(id);
        }

        public Order Delete(int id)
        {
            return _orderRepository.Delete(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
