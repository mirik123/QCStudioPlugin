using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace QuantConnect.QCPlugin
{

    /// <summary>
    /// Struct, enum iterator for the customised PairPointList input variables
    /// </summary>
    public struct dtPlot
    {
        public DateTime Time;
        public double Value;
        public int ID;
        public string Tag;
    }

    public enum dtType
    {
        Time,
        Value,
        ID
    };

    /// <summary>
    /// A customised PairPointList which enables DateTime-XAxis.
    /// </summary>
    public class DateTimePointList : IPointListEdit
    {
        // Determines what data type gets plotted for the X values
        public dtType XType;
        // Determines what data type gets plotted for the Y values
        public dtType YType;
        // Tag to store additional data about the point:
        public dtType TType;

        // Stores the collection of samples
        private List<dtPlot> aList;

        // Indexer: get the Sample instance at the specified ordinal position in the list
        public PointPair this[int index]
        {
            get
            {
                PointPair pt = new PointPair();
                dtPlot aPoint = aList[index];
                pt.X = GetValue(aPoint, XType);
                pt.Y = GetValue(aPoint, YType);
                pt.Tag = aPoint.Tag;
                return pt;
            }
            set
            {

            }
        }

        public int Count
        {
            get { return aList.Count; }
        }

        // Get the specified data type from the specified sample
        public double GetValue(dtPlot Point, dtType Type)
        {
            switch (Type)
            {
                case dtType.Time:
                    return Point.Time.ToOADate();
                case dtType.Value:
                    return Point.Value;
                case dtType.ID:
                    return Point.ID;
                default:
                    return PointPair.Missing;
            }
        }

        // Add a sample to the collection
        public void Add(dtPlot dtPoint)
        {
            aList.Add(dtPoint);
        }
        public void Add(DateTime dtTime, double dValue, int iID = 0, string sTag = "")
        {
            dtPlot dtPoint = new dtPlot();
            dtPoint.Time = dtTime;
            dtPoint.Value = dValue;
            dtPoint.ID = iID;
            dtPoint.Tag = sTag;
            aList.Add(dtPoint);
        }

        public void Add(PointPair ppData)
        {
            Add(DateTime.FromOADate(ppData.X), ppData.Y, 0, "");
        }

        //IPointListEdit Compatibility
        public void Add(double x, double y)
        {
            Add(DateTime.FromOADate(x), y, 0, "");
        }

        // Generic Clone: just call the typesafe version
        object ICloneable.Clone()
        {
            return this.Clone();
        }

        // Typesafe clone method
        public DateTimePointList Clone()
        {
            return new DateTimePointList(this);
        }

        //Clear the array:
        public void Clear()
        {
            aList.Clear();
        }

        //Remove at index i
        public void RemoveAt(int i)
        {
            aList.RemoveAt(i);
        }

        // Default constructor
        public DateTimePointList()
        {
            XType = dtType.Time;
            YType = dtType.Value;
            TType = dtType.ID;
            aList = new List<dtPlot>();
        }

        // Copy constructor
        public DateTimePointList(DateTimePointList rhs)
        {
            XType = rhs.XType;
            YType = rhs.YType;
            TType = rhs.TType;

            // Don't duplicate the data values, just copy the reference to the List
            this.aList = rhs.aList;
        }
    }
}
