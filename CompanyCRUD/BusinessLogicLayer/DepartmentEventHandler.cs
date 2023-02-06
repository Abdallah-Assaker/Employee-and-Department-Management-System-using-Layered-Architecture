using System;

namespace BusinessLogicLayer
{
    public class DepartmentEventHandler : EventArgs
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
