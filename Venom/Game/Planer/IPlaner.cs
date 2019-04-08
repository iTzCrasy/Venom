using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venom.Game.Planer
{
    /// <summary>
    /// Interface for Planers
    /// </summary>
    internal interface IPlaner
    {
        Task InitializeAsync( );    //=> Initialize Constant

        //void Start( );  //=> Start the Worker

        Task LoadAsync( );   //=> Load Plan
        Task SaveAsync( );   //=> Save Plan
    }
}
