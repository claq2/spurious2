﻿using GeoJSON.Text.Geometry;

namespace Spurious2.Subdivisions;

public class Subdivision
{
    public string Name { get; set; }
    public int Population { get; set; }
    public decimal Density { get; set; }
    public Uri BoundaryLink { get; set; }
    public Point CentreCoordinates { get; set; }
    public Uri StoresLink { get; set; }
}