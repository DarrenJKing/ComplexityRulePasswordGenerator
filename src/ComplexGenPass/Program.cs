using System;
using static System.Console;

namespace ComplexGenPass
{
    class Program
    {
        static void Main( string[ ] args )
        {
            for( int i = 0; i < 10; ++i )
            {
                var password = PasswordUtil.GenerateComplexPasswordStr( 11 );
                WriteLine( $"Generated Pass {password}" );
            }
            WriteLine( "Finished..." );
            ReadLine( );
        }
    }
}
