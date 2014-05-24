using System;
using System.Collections.Generic;
using Models;

namespace WebAPI.Models {

    interface IUserRepository {

        IEnumerable<User> GetAllUsers();
        User Get(int username);
        User Post(UserApiClass user);
        bool Delete(int mobile);
        bool Put(User user);
        IEnumerable<Reservation> GetUserReservations(int mobile);

    }

    interface IBusRespository {
        IEnumerable<Bus> GetAllBusses();
    }

    interface IReservationRespository {
        IEnumerable<Reservation> GetAllReservations();
        int DeleteReservation(int reservationId);
        Reservation PostReservation(Reservation reservation);
        Reservation PostReservation(ReservationApiClass reservation);
        Reservation GetReservation(int id);
        IEnumerable<Reservation> GetBusReservation(String regNo, DateTime date);
        IEnumerable<Reservation> GetBusReservation(String regNo);
    }

    interface ICenterRespository {
        IEnumerable<ComCenter> GetAllComCenters();
    }
}
