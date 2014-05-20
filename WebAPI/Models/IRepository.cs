using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace WebAPI.Models {
    interface IUserRepository {

        IEnumerable<User> GetAllUsers();
        User Get(int username);
        User Add(User user);
        void Remove(int mobile);
        bool Update(User user);
    }

    interface IBusRespository {
        IEnumerable<Bus> GetAllBusses();
    }

    interface IReservationRespository {
        IEnumerable<Reservation> GetAllReservations();
        void Remove(int reservationId);
        Reservation Add(Reservation reservation);
    }

    interface ICenterRespository {
        IEnumerable<ComCenter> GetAllComCenters();
    }
}
