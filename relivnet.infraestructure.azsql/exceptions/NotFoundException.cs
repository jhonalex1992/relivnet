﻿
namespace relivnet.infraestructure.azsql.exceptions
{
    public class NotFoundException : Exception
    {

        public NotFoundException()
        {
            throw new Exception("No existe un registro!");
        }
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
