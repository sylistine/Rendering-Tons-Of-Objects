# Rendering-Tons-Of-Objects

## Stats in-editor on PC (GTX1080, Unity2017.3.0f3)
601.5k verts (902.6k tris)
29 batches @15ms

## Some gifs

![32,500 Cubes, 50fps](https://raw.githubusercontent.com/sylistine/Rendering-Tons-Of-Objects/master/Examples/example2.gif)

Rendering 32,500 cubes (260,000 vertices) at over 50fps.

![Pre-optimization spherical explosion, 8125 Cubes](https://raw.githubusercontent.com/sylistine/Rendering-Tons-Of-Objects/master/Examples/example1.gif)

## Notes

Before script optimizations, 8125 cubes moving out around a source.

An application, in Unity, of some techniques outlined by
Ishibashi Seiya (@i_saint / https://github.com/i-saint)
at the Unite 2015 Tokyo conference.

Slides and video from the talk (and others can be found here:
http://japan.unity3d.com/unite/unite2015/schedule

## TODOs

- Improve API for dynamicly loading and unloading new meshes
- Add support for meshes that do not receive individual transformations each frame, but can be paired with physics colliders (similarly to Speed Tree)
