This project aims to recreate Adobe Flash editor experience by implementing Vector Networks.

Roadmap:
- [ ] Create minimal UI
- [ ] Implement basic network operations and structures
- [ ] Implement advanced network operations and structures
- [ ] Render vector network properly

Modes:
- Drawing
- Edition

Interactions:
- Drawing:
  - [ ] Click to add anchor connected to the last selected
  - [ ] And hold to add also add a control point
- Edition:
  - [ ] Click to select an anchor or a control point
  - [ ] Show control points when anchor is selected
  - [ ] Hide unselected control points when anchor is'nt selected
  - [ ] Drag anchor and control points to move them

Network operations:
- [ ] Add and modify points
- [ ] Connect and disconnect points
- [ ] Define areas by selecting edges
- [ ] Glue
- [ ] Unglue
- [ ] Cut
- [ ] Uncut

Supported features:
- [ ] Stroke width
- [ ] Butt, round and square caps and joins
- [ ] Drawing order

Further ideas:
- [ ] Automatically detect small areas
- [ ] Boolean operations


For reference:

- [Alex Harri - The Engineering behind Figma’s Vector Networks](https://alexharri.com/blog/vector-networks)
- [Boris Dalstein - Vector Graphics Complexes](https://www.borisdalstein.com/research/vgc/)