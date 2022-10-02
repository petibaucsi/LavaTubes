using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LavaTubes
{
 
    //The class represents a coordinate on the map representation. It has an x, y value, and a height
    public class Coordinate
    {

        public int x { get; set; }
        public int y { get; set; }
        public int height { get; set; }
        public bool isLowpoint { get; set; } = false;
        public Coordinate BasinOf { get; set; } = null;
        public int sizeOfBazin { get; set; } = 0;
        public bool CheckedAsBazin { get; set; } = false;

        //isLowpoint, BasinOf, sizeOfBazin

        public Coordinate(int x, int y, int height) 
        {
            this.x = x;
            this.y = y;
            this.height = height;
        }

        public void CalculatBasin(List<Coordinate> _map)
        {
            if (this.isLowpoint == true)
            {
                bool hasMoreBasin = false;
                //List<Coordinate> _basin = _map.SkipWhile(o => o.BasinOf == this && o.CheckedAsBazin == false && o.isLowpoint == false ).ToList();
                //List<Coordinate> _basin = _map.SkipWhile(o => o.BasinOf == this && o.CheckedAsBazin == false && o.isLowpoint == false).ToList();
                List<Coordinate> _basin = new List<Coordinate>();
                List<Coordinate> _newBasin = new List<Coordinate>();

                foreach (Coordinate _c in _map)
                {
                    if (_c.isLowpoint == false && _c.BasinOf == this)
                    {
                        _basin.Add(_c);
                    }
                }

                if (_basin.Count > 0)
                {
                    this.sizeOfBazin += _basin.Count;
                    hasMoreBasin = true;
                }

                //List<SomeObject> closed = objectList.SkipWhile(o => !o.Status.Equals("Closed")).ToList();
                while (hasMoreBasin == true)
                {
                    
                    //foreach (Coordinate _c in _map)
                    //{
                    //    if (_c.isLowpoint == false && _c.BasinOf == this && _c.CheckedAsBazin == false)
                    //    {
                    //        _basin.Add(_c);
                    //    }
                    //}

                    foreach (Coordinate _c in _basin)
                    {

                        Coordinate coordinateUp = _map.FirstOrDefault(c => c.x == _c.x - 1 && c.y == _c.y && c.CheckedAsBazin == false);
                        if (coordinateUp != null)
                        {
                            if (coordinateUp.height != 9 && _c.height < coordinateUp.height)
                            {
                                if (coordinateUp.BasinOf == null)
                                {
                                    coordinateUp.BasinOf = this.BasinOf;
                                    this.BasinOf.sizeOfBazin += 1;
                                }
                                _newBasin.Add(coordinateUp);

                            }
                        }
                        Coordinate coordinateDown = _map.FirstOrDefault(c => c.x == _c.x + 1 && c.y == _c.y && c.CheckedAsBazin == false);
                        if (coordinateDown != null)
                        {
                            if (coordinateDown.height != 9 && _c.height < coordinateDown.height)
                            {
                                if (coordinateDown.BasinOf == null)
                                {
                                    coordinateDown.BasinOf = this.BasinOf;
                                    this.BasinOf.sizeOfBazin += 1;
                                }
                                _newBasin.Add(coordinateDown);

                            }
                        }
                        Coordinate coordinateLeft = _map.FirstOrDefault(c => c.x == _c.x && c.y == _c.y - 1 && c.CheckedAsBazin == false);
                        if (coordinateLeft != null)
                        {
                            if (coordinateLeft.height != 9 && _c.height < coordinateLeft.height)
                            {
                                if (coordinateLeft.BasinOf == null)
                                {
                                    coordinateLeft.BasinOf = this.BasinOf;
                                    this.BasinOf.sizeOfBazin += 1;
                                }
                                _newBasin.Add(coordinateLeft);
                            }
                        }
                        Coordinate coordinateRight = _map.FirstOrDefault(c => c.x == _c.x && c.y == _c.y + 1 && c.CheckedAsBazin == false);
                        if (coordinateRight != null)
                        {
                            if (coordinateRight.height != 9 && _c.height < coordinateRight.height)
                            {
                                if (coordinateRight.BasinOf == null)
                                {
                                    coordinateRight.BasinOf = this.BasinOf;
                                    this.BasinOf.sizeOfBazin += 1;
                                }
                                _newBasin.Add(coordinateRight);
                            }
                        }
                        _c.CheckedAsBazin = true;
                        //_basin.Remove(this);
                    }
                    hasMoreBasin = _newBasin.Count > 0 ? true : false;

                    if (hasMoreBasin)
                    {
                      _basin.Clear();
                      _basin = _newBasin.ToList();
                      _newBasin.Clear();
                    }
                }
            }
        }
    }
}
