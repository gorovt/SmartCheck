/*   SmartCheck License
*******************************************************************************
*                                                                             *
*    Copyright (c) 2021 Luciano Gorosito <lucianogorosito@hotmail.com>        *
*                                                                             *
*    This file is part of SmartCheck                                          *
*                                                                             *
*    SmartCheck is free software: you can redistribute it and/or modify       *
*    it under the terms of the GNU General Public License as published by     *
*    the Free Software Foundation, either version 3 of the License, or        *
*    (at your option) any later version.                                      *
*                                                                             *
*    SmartCheck is distributed in the hope that it will be useful,            *
*    but WITHOUT ANY WARRANTY; without even the implied warranty of           *
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the            *
*    GNU General Public License for more details.                             *
*                                                                             *
*    You should have received a copy of the GNU General Public License        *
*    along with this program.  If not, see <https://www.gnu.org/licenses/>.   *
*                                                                             *
*******************************************************************************
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCheck
{
    public class UbParameter
    {
        public string name { get; set; }
        public string value { get; set; }
        public int valueType { get; set; }
        // ValueType can be:
        // 0 String
        // 1 Integer
        // 2 Double
        // 3 Bool
        // 4 ElementId

        #region Constructors
        public UbParameter()
        {
            this.name = string.Empty;
            this.value = string.Empty;
            this.valueType = 0;
        }

        /// <summary> ValueType (0: String, 1: Integer, 2: Double, 3: Bool, 4: ElementId as Integer) </summary>
        public UbParameter(string name, string value, int valueType)
        {
            this.name = name;
            this.value = value;
            this.valueType = valueType;
        }
        #endregion

        /// <summary> Get an Integer from string Value </summary>
        public int GetIntegerValue()
        {
            return Convert.ToInt32(this.value);
        }

        /// <summary> Get a Double from dtring Value </summary>
        public double GetDoubleValue()
        {
            return Convert.ToDouble(this.value);
        }

        /// <summary> Get a Boolean from the string Value </summary>
        public bool GetBooleanValue()
        {
            return Convert.ToBoolean(this.value);
        }
    }
}
