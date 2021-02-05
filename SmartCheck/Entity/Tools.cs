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
using Frm = System.Windows.Forms;
using System.IO;

namespace SmartCheck
{
    public abstract class Tools
    {
        public static string _title = "SmartCheck";
        public static string _version = "ver 0.8.5";

        public static List<Check> InitialList()
        {
            List<Check> checks = new List<Check>();
            // GENERAL
            checks.Add(new Check("", "00GEN", "GENERAL:", "GENERAL", false, "", new List<Item>(), 0));
            // Model Size < 300 MB
            checks.Add(new Check("0", "MODELSIZE", "Model size must be < 300 MB", "Model size must be < 300 MB",
                false, "", new List<Item>(), 0));
            // # of Project Information filled
            checks.Add(new Check("0", "PROJINFOFILLED", "Project Information parameters filled",
                "List of Project Information parameters filled by usr", false, "", new List<Item>(), 0));
            // # of Project Information NO filled
            checks.Add(new Check("0", "PROJINFONOFILLED", "Project Information parameters no filled",
                "List of Project Information parameters no filled", false, "", new List<Item>(), 0));
            // Project Base Point != (0,0,0)
            checks.Add(new Check("0", "BASENOZERO", "Project Base Point is (0;0;0)?",
                "Project Base Point is the origin (0;0;0)?", false, "", new List<Item>(), 0));
            // Project Survey Point != (0,0,0)
            checks.Add(new Check("0", "SURVEYNOZERO", "Project Survey Point is (0;0;0)?",
                "Project Survey Point is the origin (0;0;0)?", false, "", new List<Item>(), 0));
            // REFERENCES
            checks.Add(new Check("", "00REF", "EXTERNAL REFERENCES:", "EXTERNAL REFERENCES", false, "", new List<Item>(), 0));
            // # of CAD Imports
            checks.Add(new Check("0", "CADIMP", "CAD Imports", "Number of CAD imports", false, "", new List<Item>(), 0));
            // # of CAD Links
            checks.Add(new Check("0", "CADLINK", "CAD Links", "Number of CAD Links", false, "", new List<Item>(), 0));
            // # of Linked Revit files NO Pinned
            checks.Add(new Check("0", "RVTLNKNOPINNED", "Linked Revit files NO pinned", "Number of Linked Revit files NO pinned",
                false, "", new List<Item>(), 0));
            // VIEWS
            checks.Add(new Check("", "00VIEWS", "VIEWS:", "VIEWS", false, "", new List<Item>(), 0));
            // # of Views that are placed in Sheets
            checks.Add(new Check("0", "VIEWSINSHEETS", "Views that are placed in Sheets", 
                "List of Views that are placed in Sheets", false, "", new List<Item>(), 0));
            // # of Views that are placed in Sheets and don't have a template
            checks.Add(new Check("0", "VIEWSINSHEETSNOTEMP", "Views placed in Sheets and without template",
                "List of Views that are placed in Sheets and without template", false, "", new List<Item>(), 0));
            // # of Views that are NOT placed in Sheets
            checks.Add(new Check("0", "VIEWSNOSHEETS", "Views that are not placed in Sheets",
                "List of Views that are not placed in Sheets", false, "", new List<Item>(), 0));
            // # of ViewTemplates IN USE
            checks.Add(new Check("0", "VTINUSE", "View Templates in use", "Number of View Templates in use",
                false, "", new List<Item>(), 0));
            // # of ViewTemplates NO USE
            checks.Add(new Check("0", "VTNOUSE", "View Templates without using", "Number of View Templates without using",
                false, "", new List<Item>(), 0));
            // # of ViewFilters IN USE
            checks.Add(new Check("0", "VFINUSE", "View Filters in use", "Number of View Filters in use",
                false, "", new List<Item>(), 0));
            // # of ViewFilters NO USE
            checks.Add(new Check("0", "VFNOUSE", "View Filters without using", "Number of View Filters without using",
                false, "", new List<Item>(), 0));
            // FAMILIES
            checks.Add(new Check("", "00FAMILIES", "FAMILIES:", "FAMILIES", false, "", new List<Item>(), 0));
            // WallTypes in Use
            checks.Add(new Check("0", "WALLTYPEUSE", "Wall Types in use", "List of Wall Types in use",
                false, "", new List<Item>(), 0));
            // WallTypes without using 
            checks.Add(new Check("0", "WALLTYPENOUSE", "Wall Types without using", "List of Wall Types without using",
                false, "", new List<Item>(), 0));
            // FloorTypes in Use
            checks.Add(new Check("0", "FLOORTYPEUSE", "Floor Types in use", "List of Floor Types in use",
                false, "", new List<Item>(), 0));
            // FloorTypes without using 
            checks.Add(new Check("0", "FLOORTYPENOUSE", "Floor Types without using", "List of Floor Types without using",
                false, "", new List<Item>(), 0));
            // FamilySymbols in Use
            checks.Add(new Check("0", "SYMINUSE", "Loadable Families in use", "List of Loadable Families in use",
                false, "", new List<Item>(), 0));
            // FamilySymbols in Use
            checks.Add(new Check("0", "SYMNOUSE", "Loadable Families without using", "List of Loadable Families without using",
                false, "", new List<Item>(), 0));
            // FamilySymbols contains special characters
            checks.Add(new Check("0", "SYMSPECIALCHAR", "Loadable Families contains special characters",
                "Special Characters not allowed: @ $ % ^ < > / \\ "+ '"' + " : ; ? * | ,", false, "", new List<Item>(), 0));
            // TextNoteTypes in USE
            checks.Add(new Check("0", "TXTINUSE", "TextNote Types in use", "List of TextNote Types in use", 
                false, "", new List<Item>(), 0));
            // TextNoteTypes without using
            checks.Add(new Check("0", "TXTNOUSE", "TextNote Types without using", "List of TextNote Types without using",
                false, "", new List<Item>(), 0));
            // DiemsnionTypes in USE
            checks.Add(new Check("0", "DIMINUSE", "Dimension Types in use", "List of Dimension Types in use",
                false, "", new List<Item>(), 0));
            // DiemsnionTypes without using
            checks.Add(new Check("0", "DIMNOUSE", "Dimension Types without using", "List of Dimension Types without using",
                false, "", new List<Item>(), 0));
            // # of Sheets parameters NO filled
            // SHEETS
            checks.Add(new Check("", "00SHEETS", "SHEETS:", "SHEETS", false, "", new List<Item>(), 0));
            // No Text or Lines in Sheets
            checks.Add(new Check("0", "NODETINSHEETS", "No Text or Lines in Sheets",
                "List of Text Notes and Detail Lines in Sheets", false, "", new List<Item>(), 0));
            // # of Generic Model families

            // Set the order
            int order = 1;
            foreach (Check chk in checks)
            {
                if (chk.order != "")
                {
                    chk.order = order.ToString();
                    order++;
                }
            }
            return checks;
        }

        #region Get
        /// <summary> Get a color name from his Id </summary>
        public static string GetColorNameFromId(int colorId)
        {
            string name = "";
            switch (colorId)
            {
                case 0: // Black
                    name = "Black";
                    break;
                
                case 255:
                    name = "Red";
                    break;
                case 65280:
                    name = "Green";
                    break;
                case 65535:
                    name = "Yellow";
                    break;
                case 16711680: // Blue
                    name = "Blue";
                    break;
                case 16711935:
                    name = "Magenta";
                    break;
                case 16776960:
                    name = "Cyan";
                    break;
                case 16777215:
                    name = "White";
                    break;
                default:
                    name = colorId.ToString();
                    break;
            }
            return name;
        }

        /// <summary> Get the Project Base Point </summary>
        public static BasePoint GetProjectBasePoint()
        {
            Document doc = Main._doc;
            BasePoint point = null;
            FilteredElementCollector col = new FilteredElementCollector(doc);
            List<Element> elements = col.OfClass(typeof(BasePoint)).ToList();
            foreach (Element elem in elements)
            {
                BasePoint bPoint = (BasePoint)elem;
                if (!bPoint.IsShared)
                {
                    point = bPoint;
                }
            }
            return point;
        }

        /// <summary> Get the Survey Point </summary>
        public static BasePoint GetSurveyPoint()
        {
            Document doc = Main._doc;
            BasePoint point = null;
            FilteredElementCollector col = new FilteredElementCollector(doc);
            List<Element> elements = col.OfClass(typeof(BasePoint)).ToList();
            foreach (Element elem in elements)
            {
                BasePoint bPoint = (BasePoint)elem;
                if (bPoint.IsShared)
                {
                    point = bPoint;
                }
            }
            return point;
        }

        /// <summary> Verify if BasePoint is Zero (0,0,0) </summary>
        public static bool BasePointIsOrigin(BasePoint bPoint)
        {
            bool isOrigin = false;
            double ns = bPoint.get_Parameter(BuiltInParameter.BASEPOINT_NORTHSOUTH_PARAM).AsDouble();
            double ew = bPoint.get_Parameter(BuiltInParameter.BASEPOINT_EASTWEST_PARAM).AsDouble();
            double elev = bPoint.get_Parameter(BuiltInParameter.BASEPOINT_ELEVATION_PARAM).AsDouble();
            if (ns == 0 && ew == 0 && elev == 0)
            {
                isOrigin = true;
            }
            return isOrigin;
        }

        /// <summary> Verify if SurveyPoint is Zero (0,0,0) </summary>
        public static bool SurveyPointIsOrigin(BasePoint bPoint)
        {
            bool isOrigin = false;
            double ns = bPoint.get_Parameter(BuiltInParameter.BASEPOINT_NORTHSOUTH_PARAM).AsDouble();
            double ew = bPoint.get_Parameter(BuiltInParameter.BASEPOINT_EASTWEST_PARAM).AsDouble();
            double elev = bPoint.get_Parameter(BuiltInParameter.BASEPOINT_ELEVATION_PARAM).AsDouble();
            if (ns == 0 && ew == 0 && elev == 0)
            {
                isOrigin = true;
            }
            return isOrigin;
        }

        /// <summary> Get the Lines in the Project: DetailLines, ModelLines, DetailArcs </summary>
        public static List<Element> GetLines()
        {
            Document doc = Main._doc;
            FilteredElementCollector col = new FilteredElementCollector(doc);
            List<Element> elements = col.WhereElementIsNotElementType().ToList();
            List<Element> lines = new List<Element>();

            foreach (var elem in elements)
            {
                if (elem.Category != null && elem.Category.Id == new ElementId(BuiltInCategory.OST_Lines))
                {
                    lines.Add(elem);
                }
            }
            return lines;
        }

        /// <summary> Get the TextNotes in the Project </summary>
        public static List<Element> GetTextNotes()
        {
            Document doc = Main._doc;
            FilteredElementCollector col = new FilteredElementCollector(doc);
            List<Element> elements = col.WhereElementIsNotElementType().ToList();
            List<Element> texts = new List<Element>();

            foreach (var elem in elements)
            {
                if (elem.Category != null && elem.Category.Id == new ElementId(BuiltInCategory.OST_TextNotes))
                {
                    texts.Add(elem);
                }
            }
            return texts;
        }

        /// <summary> Get the TextNoteTypes in the Project </summary>
        public static List<TextNoteType> GetTextNoteTypes()
        {
            Document doc = Main._doc;
            FilteredElementCollector col = new FilteredElementCollector(doc);
            List<Element> elements = col.OfClass(typeof(TextNoteType)).ToList();
            List<TextNoteType> texts = new List<TextNoteType>();

            foreach (Element elem in elements)
            {
                TextNoteType type = elem as TextNoteType;
                texts.Add(type);
            }
            texts = texts.OrderBy(x => x.Name).ToList();
            return texts;
        }

        /// <summary> Get all the TextNotes in the Project </summary>
        public static List<TextNote> GetAllTextNotes()
        {
            Document doc = Main._doc;
            List<TextNote> lst = new List<TextNote>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<Element> lstElem = collector.OfClass(typeof(TextNote)).ToList();
            foreach (Element elem in lstElem)
            {
                // Convert Elements
                TextNote text = elem as TextNote;
                lst.Add(text);
            }
            return lst;
        }

        /// <summary> Get all the TextNoteType used in the Project </summary>
        public static List<TextNoteType> GetTextNoteTypesInUse()
        {
            Document doc = Main._doc;
            List<TextNoteType> types = GetTextNoteTypes();
            List<TextNote> texts = GetAllTextNotes();
            List<TextNoteType> inUse = new List<TextNoteType>();
            foreach (TextNote text in texts)
            {
                Element elem = doc.GetElement(text.GetTypeId());
                TextNoteType type = elem as TextNoteType;
                if (!inUse.Exists(x => x.Id.IntegerValue == type.Id.IntegerValue))
                {
                    inUse.Add(type);
                }
            }
            inUse = inUse.OrderBy(x => x.Name).ToList();
            return inUse;
        }

        /// <summary> Get TextNoteTypes  NO used. </summary>
        public static List<TextNoteType> GetTextNoteTypesNoUsed()
        {
            List<TextNoteType> types = GetTextNoteTypes();
            List<TextNoteType> typesInUse = GetTextNoteTypesInUse();
            List<TextNoteType> typeNoUse = new List<TextNoteType>();
            foreach (TextNoteType type in types)
            {
                if (!typesInUse.Exists(x => x.Id.IntegerValue == type.Id.IntegerValue))
                {
                    typeNoUse.Add(type);
                }
            }
            typeNoUse = typeNoUse.OrderBy(x => x.Name).ToList();
            return typeNoUse;
        }

        /// <summary> Get all the Dimmensiones in the Project </summary>
        public static List<Dimension> GetAllDimensions()
        {
            Document doc = Main._doc;
            List<Dimension> lst = new List<Dimension>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<Element> lstElem = collector.OfClass(typeof(Dimension)).ToList();
            foreach (Element elem in lstElem)
            {
                // Convert Elements
                Dimension dim = elem as Dimension;
                lst.Add(dim);
            }
            return lst;
        }

        /// <summary> Get the DimensionTypes in the Project </summary>
        public static List<DimensionType> GetDimensionTypes()
        {
            Document doc = Main._doc;
            FilteredElementCollector col = new FilteredElementCollector(doc);
            List<Element> elements = col.OfClass(typeof(DimensionType)).ToList();
            List<DimensionType> dims = new List<DimensionType>();

            foreach (Element elem in elements)
            {
                DimensionType type = elem as DimensionType;
                dims.Add(type);
            }
            dims = dims.OrderBy(x => x.Name).ToList();
            return dims;
        }

        /// <summary> Get all the DimensionType used in the Project </summary>
        public static List<DimensionType> GetDimensionTypesInUse()
        {
            Document doc = Main._doc;
            List<DimensionType> types = GetDimensionTypes();
            List<Dimension> dims = GetAllDimensions();
            List<DimensionType> inUse = new List<DimensionType>();
            foreach (Dimension dim in dims)
            {
                Element elem = doc.GetElement(dim.GetTypeId());
                DimensionType type = elem as DimensionType;
                if (!inUse.Exists(x => x.Id.IntegerValue == type.Id.IntegerValue))
                {
                    inUse.Add(type);
                }
            }
            inUse = inUse.OrderBy(x => x.Name).ToList();
            return inUse;
        }

        /// <summary> Get DimensionType  NO used. </summary>
        public static List<DimensionType> GetDimensionTypesNoUsed()
        {
            List<DimensionType> types = GetDimensionTypes();
            List<DimensionType> typesInUse = GetDimensionTypesInUse();
            List<DimensionType> typeNoUse = new List<DimensionType>();
            foreach (DimensionType type in types)
            {
                if (!typesInUse.Exists(x => x.Id.IntegerValue == type.Id.IntegerValue))
                {
                    typeNoUse.Add(type);
                }
            }
            typeNoUse = typeNoUse.OrderBy(x => x.Name).ToList();
            return typeNoUse;
        }

        /// <summary> Get the ViewSheets in the project </summary>
        public static List<ViewSheet> GetViewSheets()
        {
            Document doc = Main._doc;
            List<ViewSheet> sheets = new List<ViewSheet>();
            FilteredElementCollector col = new FilteredElementCollector(doc);
            List<Element> elements = col.OfClass(typeof(ViewSheet)).ToList();
            foreach (Element elem in elements)
            {
                ViewSheet sheet = elem as ViewSheet;
                sheets.Add(sheet);
            }
            return sheets;
        }

        /// <summary> Get all the Elements in the Sheets: Lines and TextNotes </summary>
        public static List<Element> GetElementsInSheets()
        {
            Document doc = Main._doc;
            List<Element> lines = GetLines();
            List<Element> texts = GetTextNotes();
            List<Element> all = new List<Element>();
            List<ViewSheet> sheets = GetViewSheets();
            all.AddRange(lines);
            all.AddRange(texts);

            List<Element> inSheets = new List<Element>();
            foreach (Element elem in all)
            {
                if (null != elem.OwnerViewId)
                {
                    Element view = doc.GetElement(elem.OwnerViewId);
                    if (null != view && sheets.Exists(x => x.Id == view.Id))
                    {
                        inSheets.Add(elem);
                    }
                }
            }
            return inSheets;
        }

        /// <summary> Get the Items from Project Info Parameters Filled</summary>
        public static List<Item> GetItemsFromProjectInfoFilled()
        {
            Document doc = Main._doc;
            List<Item> items = new List<Item>();
            ProjectInfo info = doc.ProjectInformation;
            foreach (Parameter param in info.Parameters)
            {
                if (!param.IsReadOnly)
                {
                    Item itm = new Item();
                    itm.name = param.Definition.Name;
                    itm.id = param.Id.IntegerValue;

                    switch (param.StorageType)
                    {
                        case StorageType.None:
                            itm.result = "NO VALUE";
                            break;
                        case StorageType.Integer:
                            itm.result = param.AsInteger().ToString();
                            itm.category = "Parameter (integer)";
                            if (!string.IsNullOrEmpty(itm.result))
                            {
                                items.Add(itm);
                            }
                            break;
                        case StorageType.Double:
                            itm.result = param.AsDouble().ToString();
                            itm.category = "Parameter (number)";
                            if (!string.IsNullOrEmpty(itm.result))
                            {
                                items.Add(itm);
                            }
                            break;
                        case StorageType.String:
                            itm.result = param.AsString();
                            itm.category = "Parameter (text)";
                            if (!string.IsNullOrEmpty(itm.result))
                            {
                                items.Add(itm);
                            }
                            break;
                        case StorageType.ElementId:
                            itm.result = param.AsElementId().IntegerValue.ToString();
                            break;
                        default:
                            break;
                    }
                }
            }
            return items;
        }

        /// <summary> Get the Items from Project Info Parameters NO Filled</summary>
        public static List<Item> GetItemsFromProjectInfoNoFilled()
        {
            Document doc = Main._doc;
            List<Item> items = new List<Item>();
            ProjectInfo info = doc.ProjectInformation;
            foreach (Parameter param in info.Parameters)
            {
                if (!param.IsReadOnly)
                {
                    Item itm = new Item();
                    itm.name = param.Definition.Name;
                    itm.id = param.Id.IntegerValue;

                    switch (param.StorageType)
                    {
                        case StorageType.None:
                            itm.result = "NO VALUE";
                            break;
                        case StorageType.Integer:
                            itm.result = param.AsInteger().ToString();
                            itm.category = "Parameter (integer)";
                            if (string.IsNullOrEmpty(itm.result))
                            {
                                itm.result = "** EMPTY **";
                                items.Add(itm);
                            }
                            break;
                        case StorageType.Double:
                            itm.result = param.AsDouble().ToString();
                            itm.category = "Parameter (number)";
                            if (string.IsNullOrEmpty(itm.result))
                            {
                                itm.result = "** EMPTY **";
                                items.Add(itm);
                            }
                            break;
                        case StorageType.String:
                            itm.result = param.AsString();
                            itm.category = "Parameter (text)";
                            if (string.IsNullOrEmpty(itm.result))
                            {
                                itm.result = "** EMPTY **";
                                items.Add(itm);
                            }
                            break;
                        case StorageType.ElementId:
                            itm.result = param.AsElementId().IntegerValue.ToString();
                            break;
                        default:
                            break;
                    }
                }
            }
            return items;
        }

        /// <summary> List of characters not allowed in descriptions </summary>
        public static List<Char> SpecialCharacters()
        {
            List<Char> chars = new List<Char>();
            chars.Add('@');
            chars.Add('$');
            chars.Add('%');
            chars.Add('^');
            //caracteres.Add('&');
            chars.Add('<');
            chars.Add('>');
            chars.Add('/');
            chars.Add('\\');
            chars.Add('"');
            chars.Add(':');
            chars.Add(';');
            chars.Add('?');
            chars.Add('*');
            chars.Add('|');
            chars.Add(',');
            return chars;
        }

        /// <summary> Check if the text contains any of the special characters </summary>
        public static List<Char> TextContainsSpecialCharacter(string text)
        {
            List<Char> chars = new List<char>();
            foreach (Char character in SpecialCharacters())
            {
                if (text.Contains(character))
                {
                    chars.Add(character);
                }
            }
            return chars;
        }

        /// <summary> Get the ImportInstance in the project </summary>
        public static List<ImportInstance> GetImportInstances()
        {
            Document doc = Main._doc;
            List<ImportInstance> imports = new List<ImportInstance>();
            FilteredElementCollector col = new FilteredElementCollector(doc);
            List<Element> elements = col.OfClass(typeof(ImportInstance)).ToList();
            foreach (Element elem in elements)
            {
                ImportInstance import = elem as ImportInstance;
                if (!import.IsLinked)
                {
                    imports.Add(import);
                }
            }
            return imports;
        }

        /// <summary> Get All the Views in the Project and in the Browser, no ViewTemplates </summary>
        public static List<View> GetAllCanPrint()
        {
            Document doc = Main._doc;
            List<View> lst = new List<View>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<Element> lstElem = collector.OfClass(typeof(View)).ToList();
            foreach (Element elem in lstElem)
            {
                // Convert Elements
                View view = elem as View;
                if (view.CanBePrinted)
                {
                    lst.Add(view);
                }
            }
            return lst;
        }

        /// <summary> Get All the Views that are placed in Sheets </summary>
        public static List<Item> GetViewsPlacedInSheets()
        {
            Document doc = Main._doc;
            List<ViewSheet> sheets = new List<ViewSheet>();
            FilteredElementCollector col = new FilteredElementCollector(doc);
            List<Element> elements = col.OfClass(typeof(ViewSheet)).ToList();
            foreach (Element elem in elements)
            {
                ViewSheet sheet = elem as ViewSheet;
                sheets.Add(sheet);
            }
            List<Item> placed = new List<Item>();
            foreach (ViewSheet sheet in sheets)
            {
                List<ElementId> onSheet = sheet.GetAllPlacedViews().ToList();
                foreach (ElementId viewId in onSheet)
                {
                    Element elem = doc.GetElement(viewId);
                    View view = elem as View;
                    Item itm = Item.ItemFromView(view);
                    itm.result = "Placed: " + sheet.SheetNumber + " - " + sheet.Name;
                    placed.Add(itm);
                }
            }
            placed = placed.OrderBy(x => x.category).ThenBy(x => x.name).ToList();
            return placed;
        }

        /// <summary> Get All the Views that are placed in Sheets and they don't have a template </summary>
        public static List<Item> GetViewsPlacedInSheetsNoTemplate()
        {
            Document doc = Main._doc;
            List<ViewSheet> sheets = new List<ViewSheet>();
            FilteredElementCollector col = new FilteredElementCollector(doc);
            List<Element> elements = col.OfClass(typeof(ViewSheet)).ToList();
            foreach (Element elem in elements)
            {
                ViewSheet sheet = elem as ViewSheet;
                sheets.Add(sheet);
            }
            List<Item> placed = new List<Item>();
            foreach (ViewSheet sheet in sheets)
            {
                List<ElementId> onSheet = sheet.GetAllPlacedViews().ToList();
                foreach (ElementId viewId in onSheet)
                {
                    Element elem = doc.GetElement(viewId);
                    View view = elem as View;
                    if (view.ViewTemplateId.IntegerValue == -1)
                    {
                        Item itm = Item.ItemFromView(view);
                        itm.result = "Placed: " + sheet.SheetNumber + " - " + sheet.Name;
                        placed.Add(itm);
                    }
                }
            }
            placed = placed.OrderBy(x => x.category).ThenBy(x => x.name).ToList();
            return placed;
        }

        /// <summary> Get All the Views that are NOT placed in Sheets </summary>
        public static List<View> GetViewsNoPlacedInSheets()
        {
            Document doc = Main._doc;
            List<ViewSheet> sheets = new List<ViewSheet>();
            FilteredElementCollector col = new FilteredElementCollector(doc);
            List<Element> elements = col.OfClass(typeof(ViewSheet)).ToList();
            foreach (Element elem in elements)
            {
                ViewSheet sheet = elem as ViewSheet;
                sheets.Add(sheet);
            }
            List<View> placed = new List<View>();
            foreach (ViewSheet sheet in sheets)
            {
                List<ElementId> onSheet = sheet.GetAllPlacedViews().ToList();
                foreach (ElementId viewId in onSheet)
                {
                    Element elem = doc.GetElement(viewId);
                    View view = elem as View;
                    placed.Add(view);
                }
            }
            List<View> notPlaced = new List<View>();
            List<View> allViews = GetAllCanPrint();
            foreach (View view in allViews)
            {
                if (view.ViewType != ViewType.DrawingSheet)
                {
                    if (!placed.Exists(x => x.Id.IntegerValue == view.Id.IntegerValue))
                    {
                        notPlaced.Add(view);
                    }
                }
            }
            notPlaced = notPlaced.OrderBy(x => x.ViewType.ToString()).ThenBy(x => x.Name).ToList();
            return notPlaced;
        }

        /// <summary> Get All the ViewTemplates in the Project </summary>
        public static List<View> GetViewTemplates()
        {
            Document doc = Main._doc;
            List<View> lst = new List<View>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<Element> lstElem = collector.OfClass(typeof(View)).ToList();
            foreach (Element elem in lstElem)
            {
                // Convert Elements
                View view = elem as View;
                if (view.IsTemplate)//.CanBePrinted && view.Category != null)
                {
                    lst.Add(view);
                }
            }
            return lst;
        }

        /// <summary> Get View Templates  in use. </summary>
        public static List<View> GetViewTemplatesInUse()
        {
            Document doc = Main._doc;
            List<View> templates = new List<View>();
            List<ElementId> ids = new List<ElementId>();
            List<View> views = GetAllCanPrint();
            foreach (View view in views)
            {
                if (view.ViewTemplateId != new ElementId(-1))
                {
                    if (!ids.Exists(x => x == view.ViewTemplateId))
                    {
                        ids.Add(view.ViewTemplateId);
                    }
                }
            }
            foreach (ElementId id in ids)
            {
                Element elem = doc.GetElement(id);
                View view = elem as View;
                templates.Add(view);
            }
            return templates;
        }

        /// <summary> Get View Templates NO used. </summary>
        public static List<View> GetViewTemplatesNoUse()
        {
            Document doc = Main._doc;
            List<View> templates = GetViewTemplates();
            List<View> templatesInUse = GetViewTemplatesInUse();
            List<View> templatesNoUse = new List<View>();
            foreach (View temp in templates)
            {
                if (!templatesInUse.Exists(x => x.Id.IntegerValue == temp.Id.IntegerValue))
                {
                    templatesNoUse.Add(temp);
                }
            }
            return templatesNoUse;
        }

        /// <summary> Get all View Filters in the project </summary>
        public static List<Element> GetFilterElements()
        {
            Document doc = Main._doc;
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<Element> lstElem = collector.OfClass(typeof(FilterElement)).ToList();
            return lstElem;
        }

        /// <summary> Get the View Filters in use </summary>
        public static List<Element> GetViewFiltersInUse()
        {
            Document doc = Main._doc;
            List<View> views = GetAllCanPrint();
            List<ElementId> ids = new List<ElementId>();
            List<Element> filters = new List<Element>();

            foreach (View view in views)
            {
                foreach (ElementId id in view.GetFilters())
                {
                    if (!ids.Exists(x => x == id))
                    {
                        ids.Add(id);
                    }
                }
            }
            foreach (ElementId id in ids)
            {
                Element elem = doc.GetElement(id);
                filters.Add(elem);
            }
            return filters;
        }

        /// <summary> Get View Filters NO used. </summary>
        public static List<Element> GetViewFiltersNoUse()
        {
            Document doc = Main._doc;
            List<Element> filters = GetFilterElements();
            List<Element> filtersInUse = GetViewFiltersInUse();
            List<Element> filtersNoUse = new List<Element>();
            foreach (Element elem in filters)
            {
                if (!filtersInUse.Exists(x => x.Id.IntegerValue == elem.Id.IntegerValue))
                {
                    filtersNoUse.Add(elem);
                }
            }
            return filtersNoUse;
        }

        /// <summary> Get the RevitLinkInstances in the project </summary>
        public static List<RevitLinkInstance> GetRevitlinkInstances()
        {
            Document doc = Main._doc;
            List<RevitLinkInstance> links = new List<RevitLinkInstance>();
            FilteredElementCollector col = new FilteredElementCollector(doc);
            List<Element> elements = col.OfClass(typeof(RevitLinkInstance)).ToList();
            foreach (Element elem in elements)
            {
                RevitLinkInstance link = elem as RevitLinkInstance;
                links.Add(link);
            }
            return links;
        }

        /// <summary> Get the RevitLinkInstances that NO Pinned in the project </summary>
        public static List<RevitLinkInstance> GetRevitLinkInstancesNoPinned()
        {
            List<RevitLinkInstance> links = GetRevitlinkInstances();
            List<RevitLinkInstance> linksNoPinned = new List<RevitLinkInstance>();
            foreach (Element elem in links)
            {
                RevitLinkInstance link = elem as RevitLinkInstance;
                if (!link.Pinned)
                {
                    linksNoPinned.Add(link);
                }
            }
            return linksNoPinned;
        }

        /// <summary> Get the size of the model, in MB </summary>
        public static double GetModelSize()
        {
            Document doc = Main._doc;
            double modelSize = 0;
            try
            {
                string path = doc.PathName;
                long size = new FileInfo(path).Length;
                modelSize = (size / 1024f) / 1024f;
            }
            catch (Exception)
            {
                // Nothing
            }
            return modelSize;
        }

        /// <summary> Get all the Walls in the Project </summary>
        public static List<Wall> GetAllWalls()
        {
            Document doc = Main._doc;
            List<Wall> lst = new List<Wall>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<Element> lstElem = collector.OfClass(typeof(Wall)).ToList();
            foreach (Element elem in lstElem)
            {
                // Convert Elements
                Wall wall = elem as Wall;
                lst.Add(wall);
            }
            return lst;
        }

        /// <summary> Get all the WallTypes loaded in the Project </summary>
        public static List<WallType> GetAllWallTypes()
        {
            Document doc = Main._doc;
            List<WallType> types = new List<WallType>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<Element> lstElem = collector.OfClass(typeof(WallType)).ToList();
            foreach (Element elem in lstElem)
            {
                // Convert Elements
                WallType type = elem as WallType;
                types.Add(type);
            }
            return types;
        }

        /// <summary> Get all the WallTypes used in the Project </summary>
        public static List<WallType> GetWallTypeInUse()
        {
            Document doc = Main._doc;
            List<WallType> types = GetAllWallTypes();
            List<Wall> walls = GetAllWalls();
            List<WallType> inUse = new List<WallType>();
            foreach (Wall wall in walls)
            {
                Element elem = doc.GetElement(wall.GetTypeId());
                WallType type = elem as WallType;
                if (!inUse.Exists(x => x.Id.IntegerValue == type.Id.IntegerValue))
                {
                    inUse.Add(type);
                }
            }
            return inUse;
        }

        /// <summary> Get WallTypes  NO used. </summary>
        public static List<WallType> GetWallTypesNoUse()
        {
            List<WallType> types = GetAllWallTypes();
            List<WallType> typesInUse = GetWallTypeInUse();
            List<WallType> typeNoUse = new List<WallType>();
            foreach (WallType type in types)
            {
                if (!typesInUse.Exists(x => x.Id.IntegerValue == type.Id.IntegerValue))
                {
                    typeNoUse.Add(type);
                }
            }
            return typeNoUse;
        }

        /// <summary> Get all the Floors in the Project </summary>
        public static List<Floor> GetAllFloors()
        {
            Document doc = Main._doc;
            List<Floor> lst = new List<Floor>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<Element> lstElem = collector.OfClass(typeof(Floor)).ToList();
            foreach (Element elem in lstElem)
            {
                // Convert Elements
                Floor floor = elem as Floor;
                lst.Add(floor);
            }
            return lst;
        }

        /// <summary> Get all the FloorTypes loaded in the Project </summary>
        public static List<FloorType> GetAllFloorTypes()
        {
            Document doc = Main._doc;
            List<FloorType> types = new List<FloorType>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<Element> lstElem = collector.OfClass(typeof(FloorType)).ToList();
            foreach (Element elem in lstElem)
            {
                // Convert Elements
                FloorType type = elem as FloorType;
                types.Add(type);
            }
            return types;
        }

        /// <summary> Get all the FloorTypes used in the Project </summary>
        public static List<FloorType> GetFloorTypeInUse()
        {
            Document doc = Main._doc;
            List<FloorType> types = GetAllFloorTypes();
            List<Floor> floors = GetAllFloors();
            List<FloorType> inUse = new List<FloorType>();
            foreach (Floor floor in floors)
            {
                Element elem = doc.GetElement(floor.GetTypeId());
                FloorType type = elem as FloorType;
                if (!inUse.Exists(x => x.Id.IntegerValue == type.Id.IntegerValue))
                {
                    inUse.Add(type);
                }
            }
            return inUse;
        }

        /// <summary> Get FloorTypes NO used. </summary>
        public static List<FloorType> GetFloorTypesNoUse()
        {
            List<FloorType> types = GetAllFloorTypes();
            List<FloorType> typesInUse = GetFloorTypeInUse();
            List<FloorType> typeNoUse = new List<FloorType>();
            foreach (FloorType type in types)
            {
                if (!typesInUse.Exists(x => x.Id.IntegerValue == type.Id.IntegerValue))
                {
                    typeNoUse.Add(type);
                }
            }
            return typeNoUse;
        }

        /// <summary> Get all FamilyInstance in the Project </summary>
        public static List<FamilyInstance> GetAllFamilyInstance()
        {
            Document doc = Main._doc;
            List<FamilyInstance> lst = new List<FamilyInstance>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<Element> lstElem = collector.OfClass(typeof(FamilyInstance)).ToList();
            foreach (Element elem in lstElem)
            {
                // Convert Elements
                FamilyInstance fam = elem as FamilyInstance;
                lst.Add(fam);
            }
            return lst;
        }

        /// <summary> Get all FamilySymbol loaded in the Project </summary>
        public static List<FamilySymbol> GetAllFamilySymbol()
        {
            Document doc = Main._doc;
            List<FamilySymbol> symbols = new List<FamilySymbol>();
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            List<Element> lstElem = collector.OfClass(typeof(FamilySymbol)).ToList();
            foreach (Element elem in lstElem)
            {
                // Convert Elements
                FamilySymbol fam = elem as FamilySymbol;
                symbols.Add(fam);
            }
            return symbols;
        }

        /// <summary> Get all the FamilySymbols used in the Project </summary>
        public static List<FamilySymbol> GetFamilySymbolInUse()
        {
            Document doc = Main._doc;
            List<FamilyInstance> fams = GetAllFamilyInstance();
            List<FamilySymbol> inUse = new List<FamilySymbol>();
            foreach (FamilyInstance fam in fams)
            {
                Element elem = doc.GetElement(fam.GetTypeId());
                FamilySymbol sym = elem as FamilySymbol;
                if (!inUse.Exists(x => x.Id.IntegerValue == sym.Id.IntegerValue))
                {
                    inUse.Add(sym);
                }
            }
            return inUse;
        }

        /// <summary> Get FamilySymbol NO used. </summary>
        public static List<FamilySymbol> GetFamilySymbolNoUse()
        {
            List<FamilySymbol> symbols = GetAllFamilySymbol();  
            List<FamilySymbol> typesInUse = GetFamilySymbolInUse();
            List<FamilySymbol> typeNoUse = new List<FamilySymbol>();
            foreach (FamilySymbol sym in symbols)
            {
                if (!typesInUse.Exists(x => x.Id.IntegerValue == sym.Id.IntegerValue))
                {
                    typeNoUse.Add(sym);
                }
            }
            return typeNoUse;
        }
        #endregion

        #region Tests
        public static void RunTests()
        {
            // # of CAD Imports
            Check chkCADIMP = Main.lstChecks.Find(x => x.code == "CADIMP");
            TestCADIMP(chkCADIMP);
            // # of CAD Links
            Check chkCADLINK = Main.lstChecks.Find(x => x.code == "CADLINK");
            TestCADLINK(chkCADLINK);
            // # of ViewTemplates IN USE
            Check chkVtInUse = Main.lstChecks.Find(x => x.code == "VTINUSE");
            TestVtInUse(chkVtInUse);
            // # of ViewTemplates NO USE
            Check chkVtNoUse = Main.lstChecks.Find(x => x.code == "VTNOUSE");
            TestVtNoUse(chkVtNoUse);
            // # of ViewFilters IN USE
            Check chkVfInUse = Main.lstChecks.Find(x => x.code == "VFINUSE");
            TestVfInUse(chkVfInUse);
            // # of ViewFilters NO USE
            Check chkVfNoUse = Main.lstChecks.Find(x => x.code == "VFNOUSE");
            TestVfNoUse(chkVfNoUse);
            // Number of Linked Revit files NO Pinned
            Check chkRvtLnkNoPinned = Main.lstChecks.Find(x => x.code == "RVTLNKNOPINNED");
            TestRvtLinkNoPInned(chkRvtLnkNoPinned);
            // Model Size < 300 MB
            Check chkModelSize = Main.lstChecks.Find(x => x.code == "MODELSIZE");
            TestModelSize(chkModelSize);
            // WallTypes IN Use
            Check chkWallTypesInUse = Main.lstChecks.Find(x => x.code == "WALLTYPEUSE");
            TestWallTypeInUse(chkWallTypesInUse);
            // WallTypes without using 
            Check chkWallTypesNoUsed = Main.lstChecks.Find(x => x.code == "WALLTYPENOUSE");
            TestWallTypeNoUsed(chkWallTypesNoUsed);
            // FloorTypes IN Use
            Check chkFloorTypesInUse = Main.lstChecks.Find(x => x.code == "FLOORTYPEUSE");
            TestFloorTypeInUse(chkFloorTypesInUse);
            // FloorTypes without using 
            Check chkFloorTypesNoUsed = Main.lstChecks.Find(x => x.code == "FLOORTYPENOUSE");
            TestFloorTypeNoUsed(chkFloorTypesNoUsed);
            // FamilySymbols IN Use
            Check chkFamilySymbolInUse = Main.lstChecks.Find(x => x.code == "SYMINUSE");
            TestFamilySymbolInUse(chkFamilySymbolInUse);
            // FamilySymbols without using 
            Check chkFamilySymbolNoUse = Main.lstChecks.Find(x => x.code == "SYMNOUSE");
            TestFamilySymbolNoUsed(chkFamilySymbolNoUse);
            // FamilySymbols contains special characters 
            Check chkFamilySymbolSpecialChar = Main.lstChecks.Find(x => x.code == "SYMSPECIALCHAR");
            TestFamilySymbolSpecialCharacter(chkFamilySymbolSpecialChar);
            // Number of Project Information filled
            Check chkProjectInfoFilled = Main.lstChecks.Find(x => x.code == "PROJINFOFILLED");
            TestProjectInfoFilled(chkProjectInfoFilled);
            // Number of Project Information NO filled
            Check chkProjectInfoNoFilled = Main.lstChecks.Find(x => x.code == "PROJINFONOFILLED");
            TestProjectInfoNoFilled(chkProjectInfoNoFilled);
            // Number of Sheets parameters NO filled
            // No Text or Lines in Sheets
            Check chkNoDetailsInSheets = Main.lstChecks.Find(x => x.code == "NODETINSHEETS");
            TestNoDetailInSheets(chkNoDetailsInSheets);
            // TextNote Types IN USE
            Check chkTextNotetypesInUse = Main.lstChecks.Find(x => x.code == "TXTINUSE");
            TestTextNoteTypesInUse(chkTextNotetypesInUse);
            // TextNote Types without using 
            Check chkTextNotetypesNoUse = Main.lstChecks.Find(x => x.code == "TXTNOUSE");
            TestTextNoteTypesNoUse(chkTextNotetypesNoUse);
            // Dimension Types IN USE
            Check chkDimensionTypesInUse = Main.lstChecks.Find(x => x.code == "DIMINUSE");
            TestDimensionTypesInUse(chkDimensionTypesInUse);
            // Dimension Types without using 
            Check chkDimensionTypesNoUse = Main.lstChecks.Find(x => x.code == "DIMNOUSE");
            TestDimensionTypesNoUse(chkDimensionTypesNoUse);
            // Project Base Point != (0,0,0)
            Check chkBasePointNoZero = Main.lstChecks.Find(x => x.code == "BASENOZERO");
            TestBasePointNoZero(chkBasePointNoZero);
            // Project Survey Point != (0,0,0)
            Check chkSurveyPointNoZero = Main.lstChecks.Find(x => x.code == "SURVEYNOZERO");
            TestSurveyPointNoZero(chkSurveyPointNoZero);
            // Views that are placed in Sheets
            Check chkViewsPlacedInSheets = Main.lstChecks.Find(x => x.code == "VIEWSINSHEETS");
            TestViewsPlacedInSheets(chkViewsPlacedInSheets);
            // Views that are placed in Sheets and without template
            Check chkViewsPlacedInSheetsNoTemp = Main.lstChecks.Find(x => x.code == "VIEWSINSHEETSNOTEMP");
            TestViewsPlacedInSheetsNoTemp(chkViewsPlacedInSheetsNoTemp);
            // Views that are NOT placed in Sheets
            Check chkViewsNotPlacedInSheets = Main.lstChecks.Find(x => x.code == "VIEWSNOSHEETS");
            TestViewsNotPlacedInSheets(chkViewsNotPlacedInSheets);
            // Number of Texts IN Sheets
            // Number of Dimmensions IN Sheets
            // Number of Generic Model families


            Frm.MessageBox.Show("All Tests completed successfully", _title, 
                Frm.MessageBoxButtons.OK, Frm.MessageBoxIcon.Information);
        }

        /// <summary> # of CAD Imports </summary>
        public static void TestCADIMP(Check check)
        {
            List<ImportInstance> imports = GetImportInstances();
            StringBuilder sb = new StringBuilder();
            foreach (ImportInstance imp in imports)
            {
                if (!imp.IsLinked)
                {
                    Item itm = Item.ItemFromImportInstance(imp);
                    check.items.Add(itm);
                }
            }
            check.result = "#" + check.items.Count.ToString();
        }

        /// <summary> # of CAD Links </summary>
        public static void TestCADLINK(Check check)
        {
            List<ImportInstance> imports = GetImportInstances();
            StringBuilder sb = new StringBuilder();
            foreach (ImportInstance imp in imports)
            {
                if (imp.IsLinked)
                {
                    Item itm = Item.ItemFromImportInstance(imp);
                    check.items.Add(itm);
                }
            }
            check.result = "#" + check.items.Count.ToString();
        }

        /// <summary> # of ViewTemplates IN USE </summary>
        public static void TestVtInUse(Check check)
        {
            List<View> templates = GetViewTemplatesInUse();
            templates = templates.OrderBy(x => x.Name).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (View vw in templates)
            {
                Item itm = Item.ItemFromView(vw);
                check.items.Add(itm);
            }
            check.result = "#" + check.items.Count.ToString();
        }

        /// <summary> # of ViewTemplates NO USE </summary>
        public static void TestVtNoUse(Check check)
        {
            List<View> templates = GetViewTemplatesNoUse();
            templates = templates.OrderBy(x => x.Name).ToList();
            foreach (View vw in templates)
            {
                Item itm = Item.ItemFromView(vw);
                check.items.Add(itm);
            }
            check.result = "#" + check.items.Count.ToString();
        }

        /// <summary> # of ViewFilters IN USE </summary>
        public static void TestVfInUse(Check check)
        {
            List<Element> filters = GetViewFiltersInUse();
            filters = filters.OrderBy(x => x.Name).ToList();
            foreach (Element elem in filters)
            {
                Item itm = Item.ItemFromElementAndCategory(elem, "View Filter");
                check.items.Add(itm);
            }
            check.result = "#" + check.items.Count.ToString();
        }

        /// <summary> # of ViewFilters without using </summary>
        public static void TestVfNoUse(Check check)
        {
            List<Element> filters = GetViewFiltersNoUse();
            filters = filters.OrderBy(x => x.Name).ToList();
            foreach (Element elem in filters)
            {
                Item itm = Item.ItemFromElementAndCategory(elem, "View Filter");
                check.items.Add(itm);
            }
            check.result = "#" + check.items.Count.ToString();
        }

        /// <summary> Number of Linked Revit files NO Pinned </summary>
        public static void TestRvtLinkNoPInned(Check check)
        {
            List<RevitLinkInstance> links = GetRevitLinkInstancesNoPinned();
            links = links.OrderBy(x => x.Name).ToList();
            foreach (RevitLinkInstance link in links)
            {
                Item itm = Item.ItemFromRevitLinkInstance(link);
                check.items.Add(itm);
            }
            check.result = "#" + check.items.Count.ToString();
        }

        /// <summary> Model Size < 300 MB </summary>
        public static void TestModelSize(Check check)
        {
            double size = GetModelSize();
            check.result = size.ToString("N1") + " MB";
        }

        /// <summary> WallTypes Used in the model </summary>
        public static void TestWallTypeInUse(Check check)
        {
            List<WallType> typesInUse = GetWallTypeInUse();
            typesInUse = typesInUse.OrderBy(x => x.FamilyName).ThenBy(x => x.Name).ToList();
            foreach (WallType type in typesInUse)
            {
                Item itm = Item.ItemFromElementAndCategory(type, type.FamilyName);
                check.items.Add(itm);
            }
            check.result = "#" + check.items.Count.ToString();
        }

        /// <summary> WallTypes NO Used in the model </summary>
        public static void TestWallTypeNoUsed(Check check)
        {
            List<WallType> typesNoUse = GetWallTypesNoUse();
            typesNoUse = typesNoUse.OrderBy(x => x.FamilyName).ThenBy(x => x.Name).ToList();
            foreach (WallType type in typesNoUse)
            {
                Item itm = Item.ItemFromElementAndCategory(type, type.FamilyName);
                check.items.Add(itm);
            }
            check.result = "#" + check.items.Count.ToString();
        }

        /// <summary> Floortypes Used in the model </summary>
        public static void TestFloorTypeInUse(Check check)
        {
            List<FloorType> typesInUse = GetFloorTypeInUse();
            typesInUse = typesInUse.OrderBy(x => x.FamilyName).ThenBy(x => x.Name).ToList();
            foreach (FloorType type in typesInUse)
            {
                Item itm = Item.ItemFromElementAndCategory(type, type.FamilyName);
                check.items.Add(itm);
            }
            check.result = "#" + check.items.Count.ToString();
        }

        /// <summary> FloorTypes NO Used in the model </summary>
        public static void TestFloorTypeNoUsed(Check check)
        {
            List<FloorType> typesNoUse = GetFloorTypesNoUse();
            typesNoUse = typesNoUse.OrderBy(x => x.FamilyName).ThenBy(x => x.Name).ToList();
            foreach (FloorType type in typesNoUse)
            {
                Item itm = Item.ItemFromElementAndCategory(type, type.FamilyName);
                check.items.Add(itm);
            }
            check.result = "#" + check.items.Count.ToString();
        }

        /// <summary> FamilySymbols Used in the model </summary>
        public static void TestFamilySymbolInUse(Check check)
        {
            List<FamilySymbol> famInUse = GetFamilySymbolInUse();
            famInUse = famInUse.OrderBy(x => x.Category.Name).ThenBy(x => x.FamilyName).ToList();
            foreach (FamilySymbol sym in famInUse)
            {
                Item itm = Item.ItemFromFamilySymbol(sym);
                check.items.Add(itm);
            }
            check.result = "#" + check.items.Count.ToString();
        }

        /// <summary> FamilySymbols NO Used in the model </summary>
        public static void TestFamilySymbolNoUsed(Check check)
        {
            List<FamilySymbol> typesNoUse = GetFamilySymbolNoUse();
            typesNoUse = typesNoUse.OrderBy(x => x.Category.Name).ThenBy(x => x.FamilyName).ToList();
            foreach (FamilySymbol type in typesNoUse)
            {
                Item itm = Item.ItemFromFamilySymbol(type);
                check.items.Add(itm);
            }
            check.result = "#" + check.items.Count.ToString();
        }

        /// <summary> FamilySymbols contains special characters </summary>
        public static void TestFamilySymbolSpecialCharacter(Check check)
        {
            List<FamilySymbol> fams = GetAllFamilySymbol();
            fams = fams.OrderBy(x => x.Category.Name).ThenBy(x => x.FamilyName).ToList();
            foreach (FamilySymbol sym in fams)
            {
                Item itm = Item.ItemFromFamilySymbol(sym);
                List<Char> chars = TextContainsSpecialCharacter(sym.FamilyName);
                chars.AddRange(TextContainsSpecialCharacter(sym.Name));
                if (chars.Count > 0)
                {
                    itm.result = "Characters: ";
                    foreach (Char chara in chars)
                    {
                        itm.result += chara + " ";
                    }
                    check.items.Add(itm);
                }
            }
            check.result = "#" + check.items.Count.ToString();
        }

        // Number of Project Information filled
        public static void TestProjectInfoFilled(Check check)
        {
            List<Item> items = GetItemsFromProjectInfoFilled();
            items = items.OrderBy(x => x.name).ToList();
            check.items.AddRange(items);
            check.result = "#" + check.items.Count.ToString();
        }

        // Number of Project Information NO filled
        public static void TestProjectInfoNoFilled(Check check)
        {
            List<Item> items = GetItemsFromProjectInfoNoFilled();
            items = items.OrderBy(x => x.name).ToList();
            check.items.AddRange(items);
            check.result = "#" + check.items.Count.ToString();
        }

        // No Detail Lines or Text Notes in Sheets
        public static void TestNoDetailInSheets(Check check)
        {
            Document doc = Main._doc;
            List<Element> elems = GetElementsInSheets();
            List<ViewSheet> sheets = GetViewSheets();
            List<Item> items = new List<Item>();
            foreach (Element elem in elems)
            {
                Element view = doc.GetElement(elem.OwnerViewId);
                ViewSheet sheet = sheets.FirstOrDefault(x => x.Id == view.Id);
                Item itm = Item.ItemFromElement(elem);
                itm.result = "Sheet: " + sheet.SheetNumber + " " + sheet.Name;
                items.Add(itm);
            }
            items = items.OrderBy(x => x.result).ThenBy(x => x.name).ToList();
            check.items.AddRange(items);
            check.result = "#" + check.items.Count.ToString();
        }

        // TextNote Types IN USE
        public static void TestTextNoteTypesInUse(Check check)
        {
            List<TextNoteType> inUse = GetTextNoteTypesInUse();
            List<Item> items = new List<Item>();
            foreach (TextNoteType type in inUse)
            {
                Item itm = Item.ItemFromTextNoteType(type);
                items.Add(itm);
            }
            items = items.OrderBy(x => x.category).ThenBy(x => x.name).ToList();
            check.items.AddRange(items);
            check.result = "#" + check.items.Count.ToString();
        }

        // TextNote Types NO USED
        public static void TestTextNoteTypesNoUse(Check check)
        {
            List<TextNoteType> noUsed = GetTextNoteTypesNoUsed();
            foreach (TextNoteType type in noUsed)
            {
                Item itm = Item.ItemFromTextNoteType(type);
                check.items.Add(itm);
            }
            check.result = "#" + check.items.Count.ToString();
        }

        // Dimension Types IN USE
        public static void TestDimensionTypesInUse(Check check)
        {
            List<DimensionType> inUse = GetDimensionTypesInUse();
            List<Item> items = new List<Item>();
            foreach (DimensionType type in inUse)
            {
                Item itm = Item.ItemFromDimensionType(type);
                items.Add(itm);
            }
            items = items.OrderBy(x => x.category).ThenBy(x => x.name).ToList();
            check.items.AddRange(items);
            check.result = "#" + check.items.Count.ToString();
        }

        // Dimension Types NO USED
        public static void TestDimensionTypesNoUse(Check check)
        {
            List<DimensionType> noUsed = GetDimensionTypesNoUsed();
            List<Item> items = new List<Item>();
            foreach (DimensionType type in noUsed)
            {
                Item itm = Item.ItemFromDimensionType(type);
                items.Add(itm);
            }
            items = items.OrderBy(x => x.category).ThenBy(x => x.name).ToList();
            check.items.AddRange(items);
            check.result = "#" + check.items.Count.ToString();
        }

        // Project Base Point is ZERO
        public static void TestBasePointNoZero(Check check)
        {
            BasePoint bs = GetProjectBasePoint();
            bool isZero = BasePointIsOrigin(bs);
            Item itm = Item.ItemFromBasePoint(bs);
            if (isZero)
            {
                itm.result = "IS ZERO";
            }
            else
            {
                itm.result = "IS NOT ZERO";
            }
            check.items.Add(itm);
            check.result = isZero.ToString();
        }

        // Project Survey Point is ZERO
        public static void TestSurveyPointNoZero(Check check)
        {
            BasePoint bs = GetSurveyPoint();
            bool isZero = SurveyPointIsOrigin(bs);
            Item itm = Item.ItemFromBasePoint(bs);
            if (isZero)
            {
                itm.result = "IS ZERO";
            }
            else
            {
                itm.result = "IS NOT ZERO";
            }
            check.items.Add(itm);
            check.result = isZero.ToString();
        }

        // Views that are placed in sheets
        public static void TestViewsPlacedInSheets(Check check)
        {
            List<Item> placed = GetViewsPlacedInSheets();
            check.items.AddRange(placed);
            check.result = "#" + check.items.Count.ToString();
        }

        // Views that are placed in sheets and without template
        public static void TestViewsPlacedInSheetsNoTemp(Check check)
        {
            List<Item> placed = GetViewsPlacedInSheetsNoTemplate();
            check.items.AddRange(placed);
            check.result = "#" + check.items.Count.ToString();
        }

        // Views that are NOT placed in sheets
        public static void TestViewsNotPlacedInSheets(Check check)
        {
            List<View> notPlaced = GetViewsNoPlacedInSheets();
            foreach (View view in notPlaced)
            {
                Item itm = Item.ItemFromView(view);
                itm.result = "Not placed in Sheets";
                check.items.Add(itm);
            }
            check.result = "#" + check.items.Count.ToString();
        }
        #endregion

        #region Export
        /// <summary> Save an html File </summary>
        public static void SaveHtmlFile(string fileName, string code)
        {
            File.WriteAllText(fileName, code);
        }

        /// <summary> Copy the style.css file </summary>
        public static void CopyStyles(string path)
        {
            string source = Path.Combine(Path.GetDirectoryName(App.ExecutingAssemblyPath), "styles.css");
            File.Copy(source, Path.Combine(Path.GetDirectoryName(path), "styles.css"), true);
        }

        /// <summary> Export a Check to a Html file </summary>
        public static void ExportCheck(Check check)
        {
            Document doc = Main._doc;
            string path = string.Empty;
            System.Windows.Forms.SaveFileDialog save = new System.Windows.Forms.SaveFileDialog();
            save.Filter = "Html document|*.html";
            string title = doc.Title;
            save.FileName = title + "-" + check.order + "-" + check.name + "-Report.html";
            System.Windows.Forms.DialogResult result = save.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                path = save.FileName;
                try
                {
                    // Cretae Html code
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("<html>");
                    //sb.AppendLine("<head>");
                    //sb.AppendLine("<link rel='stylesheet' href='styles.css'>");
                    //sb.AppendLine("</head>");
                    sb.AppendLine("<body>");
                    sb.AppendLine("<h1>" + title + "</h1>");
                    sb.AppendLine(check.ToHtml());
                    sb.AppendLine("</body>");
                    sb.AppendLine("</html>");
                    string code = sb.ToString();
                    SaveHtmlFile(path, code);
                    //CopyStyles(path);
                    System.Windows.Forms.MessageBox.Show("File saved successfully", _title,
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    System.Diagnostics.Process.Start(path);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.ToString(), _title,
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
        }

        /// <summary> Export all Checks to a Html file </summary>
        public static void ExportAllChecks()
        {
            Document doc = Main._doc;
            string path = string.Empty;
            System.Windows.Forms.SaveFileDialog save = new System.Windows.Forms.SaveFileDialog();
            save.Filter = "Html document|*.html";
            string title = doc.Title;
            save.FileName = title + "-" + "-Full Report.html";
            System.Windows.Forms.DialogResult result = save.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                path = save.FileName;
                try
                {
                    // Cretae Html code
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("<html>");
                    //sb.AppendLine("<head>");
                    //sb.AppendLine("<link rel='stylesheet' href='styles.css'>");
                    //sb.AppendLine("</head>");
                    sb.AppendLine("<body>");
                    sb.AppendLine("<h1>" + title + "</h1>");
                    foreach (Check chk in Main.lstChecks)
                    {
                        if (chk.order != "")
                        {
                            sb.AppendLine(chk.ToHtml());
                        }
                    }
                    sb.AppendLine("</body>");
                    sb.AppendLine("</html>");
                    string code = sb.ToString();
                    SaveHtmlFile(path, code);
                    //CopyStyles(path);
                    System.Windows.Forms.MessageBox.Show("File saved successfully", _title,
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    System.Diagnostics.Process.Start(path);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Error: " + ex.ToString(), _title,
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
        }
        #endregion
    }
}
