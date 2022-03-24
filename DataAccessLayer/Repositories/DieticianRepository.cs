﻿using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class DieticianRepository : IStatusRepository<Dietician>, ILogin<DieticianRegisterInfo>
    {
        DietProgramContext db;
        public DieticianRepository()
        {
            db = new DietProgramContext();
        }   

        public bool AddInformation(Dietician dietician)
        {
            db.Dieticians.Add(dietician);

            return db.SaveChanges() > 0;
        }

        public bool AddRegister(DieticianRegisterInfo dietician)
        {
            db.DieticianRegisterInfos.Add(dietician);

            return db.SaveChanges() > 0;
        }

        public DieticianRegisterInfo CheckLogin(string email, string password)
        {
            DieticianRegisterInfo dietician = db.DieticianRegisterInfos.Where(d => d.Email == email && d.Password == password).SingleOrDefault();

            if (dietician.Dietician.Status == "Active")
            {
                return dietician;
            }
            else
            {
                return null;
            }
        }

        public void Active(Dietician entity)
        {
            Dietician dietician = db.Dieticians.Find(entity.Id);
            dietician.Status = "Active";

            db.SaveChanges();
        }

        public void Passive(Dietician entity)
        {
            Dietician dietician = db.Dieticians.Find(entity.Id);
            dietician.Status = "Passive";

            db.SaveChanges();
        }
    }
}
