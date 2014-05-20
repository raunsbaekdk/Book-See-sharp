using System.Collections.Generic;
using Models;

namespace WebAPI.Models {
    interface IUserRepository {

        IEnumerable<User> GetAllUsers();
        User Get(int username);
        User Add(User user);
        void DeleteUser(int mobile);
        bool PutUser(User user);
    }

    interface IBusRespository {
        IEnumerable<Bus> GetAllBusses();
    }

    interface IReservationRespository {
        IEnumerable<Reservation> GetAllReservations();
        void DeleteReservation(int reservationId);
        Reservation PostReservation(Reservation reservation);
        Reservation GetReservation(int id);
    }

    interface ICenterRespository {
        IEnumerable<ComCenter> GetAllComCenters();
    }
}
