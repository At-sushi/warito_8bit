using System;
using System.Collections.Generic;
using System.Text;

namespace gazo
{
    interface IGazoController
    {
        bool Process(string src, string dst);
    }
}
