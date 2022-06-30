using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cashpoint.Models;

namespace Cashpoint.Controllers
{
    public class HomeController : Controller
    {
        private List<int> _result;
        private List<int> _nominals;
        private List<int> _nominalsCount;
        private Models.Cashpoint _cashPoint;
        public ActionResult Index()
        {
            initCashpoint();
            return View(_cashPoint);
        }

        [HttpPost]
        public ActionResult IsCashGiveable(Models.Cashpoint someCashPoint, int? cash)
        {
            _result = new List<int>(someCashPoint.TapeNominals);
            for (int i = 0; i < _result.Count; i++)
            {
                _result[i] = 0;
            }
            _nominals = new List<int>(someCashPoint.TapeNominals);
            _nominalsCount = new List<int>(someCashPoint.NominalsCount);
            List<int> _copy = new List<int>(_result);
            subtractRecursively(_copy, cash);
            List<List<int>> concat = new List<List<int>>(2);
            for (int i = 0; i < _result.Count; i++)
            {
                concat.Add(new List<int>() { _result[i], _nominals[i] });
            }
            return PartialView("Tick", concat);
        }

        private void subtractRecursively(List<int> result, int? cash)
        {
            if (!checkAvailability(result))
            {
                return;
            }
            for (int i = 0; i < _nominals.Count; i++)
            {
                cash -= _nominals[i];
                result[i]++;
                if (cash > 0)
                {
                    subtractRecursively(result, cash);
                    if (checkResult())
                    {
                        return;
                    }
                }
                else if(cash == 0)
                {
                    if (checkAvailability(result))
                    {
                        _result = new List<int>(result);
                        return;
                    }
                }
                cash += _nominals[i];
                result[i]--;
            }
        }

        private bool checkAvailability(List<int> someResult)
        {
            for(int i = 0; i < someResult.Count; i++)
            {
                if (someResult[i] > _nominalsCount[i])
                {
                    return false;
                }
            }
            return true;
        }

        private bool checkResult()
        {
            foreach(var r in _result)
            {
                if(r != 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void initCashpoint()
        {
            _cashPoint = new Models.Cashpoint();
            _cashPoint.TapeCount = 1;
            _cashPoint.TapeNominals = new List<int>() { 1000 };
            _cashPoint.NominalsCount = new List<int>() { 5 };
            _cashPoint.TapeStatuses = new List<bool>() { true };
        }
    }
}