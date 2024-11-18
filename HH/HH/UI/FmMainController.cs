using HH.Models;
using Microsoft.VisualBasic.ApplicationServices;
using ReaLTaiizor.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Views
{
    public class FmMainController
    {
        private readonly UCMain uFMain;
        private readonly UserRepository _repository;

        public FmMainController(UCMain view)
        {
            uFMain = view;
            _repository = new UserRepository();
            uFMain.FmMainCont = this;
        }

        public void LoadUser(int userId)
        {
            var user = _repository.GetUsers().Where(a => a.id == "admin").FirstOrDefault();
            
        }

        public void showGridView()
        {
            var user = _repository.GetUsers();
         
        }

    }



    public class UserRepository
    {
        public List<LoginModel> GetUsers()
        {
            // 데이터베이스에서 사용자 데이터를 가져오는 로직 (예시로 하드코딩된 데이터를 반환)
            return new List<LoginModel>
            {
                new LoginModel { index = 1, id = "admin", password = "1234" },
                new LoginModel { index = 2, id = "testuser1", password = "1234" },
                new LoginModel { index = 3, id = "testuser2", password = "1234" },
                new LoginModel { index = 4, id = "testuser3", password = "12345" }
            };

        }
    }
}
