using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ирида
{
    static public class DB
    {
        static public DBModel1Container db = new DBModel1Container();
        public static List<Clients> ClientsList { get; set; } = GetClients();
        public static List<Clients> GetClients()
        {
            db.ClientsSet.Load();
            return db.ClientsSet.ToList();
        }
        public static List<Rooms> RoomsList { get; set; } = GetRooms();
        public static List<Rooms> GetRooms()
        {
            db.RoomsSet.Load();
            return db.RoomsSet.ToList();
        }
        public static List<Reservations> ReservationsList { get; set; } = GetReservations();

        public static List<Reservations> GetReservations()
        {
            db.ReservationsSet.Load();
            return db.ReservationsSet.ToList();
        }
        public static List<Accommodations> AccommodationsList { get; set; } = GetAccommodations();
        public static List<Accommodations> GetAccommodations()
        {
            db.AccommodationsSet.Load();
            return db.AccommodationsSet.ToList();
        }
        public static void Save()
        {
            db.SaveChanges();
            AccommodationsList = GetAccommodations();
            ReservationsList = GetReservations();
            ClientsList = GetClients();
        }
        public static void AddClient(Clients clients)
        {
            db.ClientsSet.Add(clients);
            db.SaveChanges();
            ClientsList = GetClients();
        }
        public static void DeleteClient(Clients clients)
        {
            db.ClientsSet.Remove(clients);
            db.SaveChanges();
            ClientsList = GetClients();
        }
        public static void AddReservation(Reservations reservations)
        {
            db.ReservationsSet.Add(reservations);
            db.SaveChanges();
            ReservationsList = GetReservations();

        }
        public static void DeleteReservation(Reservations reservations)
        {
            db.ReservationsSet.Remove(reservations);
            db.SaveChanges();
            ReservationsList = GetReservations();
        }
        public static void AddAccommodation(Accommodations accommodations)
        {
            db.AccommodationsSet.Add(accommodations);
            db.SaveChanges();
            AccommodationsList = GetAccommodations();

        }
        public static void DeleteAccommodation(Accommodations accommodations)
        {
            db.AccommodationsSet.Remove(accommodations);
            db.SaveChanges();
            AccommodationsList = GetAccommodations();
        }
    }
}
 