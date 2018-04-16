using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComplexGenPass
{
    public class PasswordUtil
    {
        public static string GenerateComplexPasswordStr( int length = 12 )
        {
            return string.Concat( GenerateComplexPassword( length ) );
        }

        public static char[ ] GenerateComplexPassword( int length = 12 )
        {
            // The following algorithm will make certain that
            // at least one character from each of the sections in the passchars
            // array is included. 
            var passchars = new string[ ]
            {
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                "abcdefghijklmnopqrstuvwxyz",
                "0123456789",
                "!@#$%^&*~=+",
            };
            var intIndex = Enumerable.Repeat( 1, passchars.Length ).ToArray( );
            var password = new char[ length ];
            var randomIndexArray = CreateRandomIndexArray( length );
            Random random = new Random( ( int )DateTime.Now.Ticks );

            // Every section needs at least 1 choice
            int max = length - passchars.Length;
            int sectionCharacterCount = 0;
            int passwordPosIndex = 0;
            int sectionIndex = 0;
            string passcharsection = string.Empty;

            while( sectionIndex < passchars.Length && max >= 1 )
            {
                sectionCharacterCount = random.Next( 1, max );
                intIndex[ sectionIndex ] = sectionCharacterCount;
                if( intIndex.Sum( ) + max - 1 > length )
                {
                    max = length - intIndex.Sum( ) + 1;
                }
                // Final part is special needs to fill up remaineder if there is one.
                if( sectionIndex == passchars.Length - 1 )
                {
                    // This will run for the last section because if max has more than 1 item
                    // it needs to be adjusted for the remaineder left over to add up to the length
                    if( max > 1 )
                    {
                        intIndex[ sectionIndex ] += length - intIndex.Sum( );
                        sectionCharacterCount = intIndex[ sectionIndex ];
                    }
                }
                passcharsection = passchars[ sectionIndex++ ];
                for( int pcount = 0; pcount < sectionCharacterCount; ++pcount )
                {
                    password[ randomIndexArray[ passwordPosIndex++ ] ] = passcharsection[ random.Next( passcharsection.Length - 1 ) ];
                }
            }
            while( sectionIndex < passchars.Length )
            {
                passcharsection = passchars[ sectionIndex++ ];
                password[ randomIndexArray[ passwordPosIndex++ ] ]
                    = passcharsection[ random.Next( passcharsection.Length - 1 ) ];
            }

            return password;
        }

        private static int[ ] CreateRandomIndexArray( int length )
        {
            // 55000 picked because what ever make sure the seed isn't the same as previous method
            Random random = new Random( ( int )DateTime.Now.Ticks - 55000 );
            int[ ] start = Enumerable.Range( 0, length ).ToArray( );
            for( int indexMark = length - 1; indexMark > 0; --indexMark )
            {
                int randomIndex = random.Next( indexMark );
                var val = start[ indexMark ];
                start[ indexMark ] = start[ randomIndex ];
                start[ randomIndex ] = val;
            }
            return start;
        }
    }
}
