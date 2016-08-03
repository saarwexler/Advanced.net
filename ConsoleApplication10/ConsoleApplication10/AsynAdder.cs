using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication10
{
    class AsynAdder
    {
        private int _x = 0;
        private int _counter = 0;
        private TaskCompletionSource<int>  _source = new TaskCompletionSource<int>();
        
       
        public Task<int> Add(int x)
        {
            _counter++;
            _x += x;
            if(_counter == 3)
                _source.SetResult(_x);
            return _source.Task;
        }




    }
}
