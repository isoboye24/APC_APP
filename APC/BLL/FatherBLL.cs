﻿using APC.DAL.DAO;
using APC.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.BLL
{
    public class FatherBLL
    {
        FatherDAO dao = new FatherDAO();

        public FatherDTO Select()
        {
            FatherDTO dto = new FatherDTO();
            dto.Fathers = dao.Select();
            return dto;
        }
    }
}
