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
using Autodesk.Revit.DB;

namespace SmartCheck
{
    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string result { get; set; }

        public Item()
        {
            this.id = 0;
            this.category = string.Empty;
            this.name = string.Empty;
            this.result = string.Empty;
        }

        public Item(int id, string category, string name, string result)
        {
            this.id = id;
            this.category = category;
            this.name = name;
            this.result = result;
        }

        /// <summary> Create an Item from an Element </summary>
        public static Item ItemFromElement(Element elem)
        {
            Item item = new Item();
            item.id = elem.Id.IntegerValue;
            // Get the Name
            try
            {
                string name = elem.get_Parameter(BuiltInParameter.ELEM_FAMILY_AND_TYPE_PARAM).AsValueString();
                if (name != string.Empty)
                {
                    item.name = name;
                }
                else
                {
                    item.name = elem.Name;
                }
                if (elem.Category.Id == new ElementId(BuiltInCategory.OST_Views))
                {
                    item.name = elem.Name;
                }
            }
            catch (Exception)
            {
                item.name = elem.Name;
            }
            // Get the category
            try
            {
                item.category = elem.Category.Name;
            }
            catch (Exception)
            {
                item.category = "-";
            }
            return item;
        }

        /// <summary> Create an Item from an Element and a Category Name</summary>
        public static Item ItemFromElementAndCategory(Element elem, string category)
        {
            Item item = new Item();
            item.id = elem.Id.IntegerValue;
            // Get the Name
            try
            {
                string name = elem.get_Parameter(BuiltInParameter.ELEM_FAMILY_AND_TYPE_PARAM).AsValueString();
                if (name != string.Empty)
                {
                    item.name = name;
                }
                else
                {
                    item.name = elem.Name;
                }
            }
            catch (Exception)
            {
                item.name = elem.Name;
            }
            item.category = category;
            return item;

        }

        /// <summary> Create an Item from a Workset </summary>
        public static Item ItemFromWorkset(Workset work)
        {
            Item item = new Item();
            item.id = 0;
            item.name = work.Name;
            item.category = "Workset";
            return item;
        }

        /// <summary> Create an Item from a Region </summary>
        public static Item ItemFromFilledRegionType(FilledRegionType region)
        {
            Item item = new Item();
            item.id = 0;
            item.name = region.Name;
            item.category = "Filled Region";
            return item;
        }

        /// <summary> Create an Item from a FamilySymbol </summary>
        public static Item ItemFromFamilySymbol(FamilySymbol fam)
        {
            Item item = new Item();
            item.id = fam.Id.IntegerValue;
            item.name = fam.FamilyName + ": " + fam.Name;
            item.category = fam.Category.Name;
            return item;
        }

        /// <summary> Create an Item from an ImportInstance </summary>
        public static Item ItemFromImportInstance(ImportInstance import)
        {
            StringBuilder sb = new StringBuilder();
            Item item = new Item();
            item.id = 0;
            Parameter param = import.get_Parameter(BuiltInParameter.IMPORT_SYMBOL_NAME);
            item.name = param.AsString();
            if (import.IsLinked)
            {
                item.category = "Linked file";
            }
            else
            {
                item.category = "Import instance";
            }
            if (import.OwnerViewId != (new ElementId(-1)))
            {
                Element elem = import.Document.GetElement(import.OwnerViewId);
                View view = (View)elem;
                sb.AppendLine("Host View: " + view.Name);
                //item.category += " | Host View: " + view.Name;
            }
            else
            {
                sb.AppendLine("No Host View");
                //item.category += " | No host View ";
            }
            // File Path
            Element type = import.Document.GetElement(import.GetTypeId());
            CADLinkType cadType = type as CADLinkType;
            if (type.IsExternalFileReference())
            {
                ExternalFileReference fileRef = cadType.GetExternalFileReference();
                if (fileRef != null)
                {
                    ModelPath mPath = fileRef.GetAbsolutePath();
                    string path = ModelPathUtils.ConvertModelPathToUserVisiblePath(mPath);
                    sb.AppendLine("File Path: " + path);
                }
            }
            item.result = sb.ToString();
            return item;
        }

        /// <summary> Create an Item from a RevitLinkInstance </summary>
        public static Item ItemFromRevitLinkInstance(RevitLinkInstance link)
        {
            StringBuilder sb = new StringBuilder();
            Item item = new Item();
            item.id = link.Id.IntegerValue;
            item.name = link.Name;
            item.category = "Revit Link Instance";
            // Path
            Element type = link.Document.GetElement(link.GetTypeId());
            RevitLinkType rvtType = type as RevitLinkType;
            if (type.IsExternalFileReference())
            {
                ExternalFileReference fileRef = rvtType.GetExternalFileReference();
                if (fileRef != null)
                {
                    ModelPath mPath = fileRef.GetAbsolutePath();
                    string path = ModelPathUtils.ConvertModelPathToUserVisiblePath(mPath);
                    sb.AppendLine("File Path: " + path);
                }
            }
            item.result = sb.ToString();
            return item;
        }

        /// <summary> Create an Item from a TextNoteType </summary>
        public static Item ItemFromTextNoteType(TextNoteType texto)
        {
            Item item = new Item();
            item.id = texto.Id.IntegerValue;
            item.name = texto.Name;
            item.category = "TextNote";
            // Font parameters
            string font = texto.get_Parameter(BuiltInParameter.TEXT_FONT).AsString();
            string size = texto.get_Parameter(BuiltInParameter.TEXT_SIZE).AsValueString();
            item.result = "Font: " + font + " || FontSize: " + size;
            if (texto.get_Parameter(BuiltInParameter.TEXT_STYLE_BOLD).AsInteger() == 1)
            {
                item.result += " BOLD";
            }
            if (texto.get_Parameter(BuiltInParameter.TEXT_STYLE_ITALIC).AsInteger() == 1)
            {
                item.result += " ITALIC";
            }
            return item;
        }

        /// <summary> Create an Item from a DimensionType </summary>
        public static Item ItemFromDimensionType(DimensionType dimm)
        {
            Item item = new Item();
            item.id = dimm.Id.IntegerValue;
            item.name = dimm.Name;
            item.category = "Dimension Type";
            string font = dimm.get_Parameter(BuiltInParameter.TEXT_FONT).AsString();
            string size = dimm.get_Parameter(BuiltInParameter.TEXT_SIZE).AsValueString();
            ElementId marcaId = dimm.get_Parameter(BuiltInParameter.DIM_LEADER_ARROWHEAD).AsElementId();
            Element marca = dimm.Document.GetElement(marcaId);
            string arrow = "";
            if (marca == null)
            {
                arrow = "NO MARK";
            }
            else
            {
                arrow = marca.Name;
            }
            item.result = "Font: " + font + " || FontSize: " + size + " || Mark: " + arrow;
            return item;
        }

        /// <summary> Create an Item from a Family </summary>
        public static Item ItemFromFamily(Family fam)
        {
            Item item = new Item();
            item.id = 0;// fam.Id.IntegerValue;
            item.name = fam.Name;
            item.category = fam.FamilyCategory.Name;
            return item;
        }

        /// <summary> Create an Item from Name and Category </summary>
        public static Item ItemFromString(string name, string category)
        {
            Item item = new Item();
            item.id = 0;
            item.name = name;
            item.category = category;
            return item;
        }

        /// <summary> Create an Item from a View </summary>
        public static Item ItemFromView(View view)
        {
            Item item = new Item();
            item.id = view.Id.IntegerValue;
            item.name = view.Name;
            item.category = view.ViewType.ToString();
            item.result = "-";
            return item;
        }

        /// <summary> Create an Item from a BasePoint </summary>
        public static Item ItemFromBasePoint(BasePoint bPoint)
        {
            Item item = new Item();
            item.id = 0;
            string ns = bPoint.get_Parameter(BuiltInParameter.BASEPOINT_NORTHSOUTH_PARAM).AsValueString();
            string ew = bPoint.get_Parameter(BuiltInParameter.BASEPOINT_EASTWEST_PARAM).AsValueString();
            string elev = bPoint.get_Parameter(BuiltInParameter.BASEPOINT_ELEVATION_PARAM).AsValueString();
            item.name = "(N/S " + ns + ", E/W " + ew + ", Elev " + elev + ")";
            if (bPoint.IsShared)
            {
                item.category = "Survey Point";
            }
            else
            {
                item.category = "Project Base Point";
            }
            return item;
        }

        /// <summary> Create an Item from a ViewSchedule </summary>
        public static Item ItemFromViewSchedule(ViewSchedule schedule)
        {
            Item item = new Item();
            item.id = 0;
            item.name = schedule.Name;
            item.category = schedule.ViewType.ToString();
            return item;
        }

        /// <summary> Create an Item from a Category </summary>
        public static Item ItemFromCategory(Category cat)
        {
            Item item = new Item();
            item.id = 0;
            item.name = cat.Name;
            item.category = "Category";
            return item;
        }
    }
}
