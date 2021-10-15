export function setSliders(state, sliders) {
  state.sliders = sliders;
}

export function addSlider(state, slider) {
  state.sliders.push(slider);
}

export function removeSlider(state, id) {
  let index = state.sliders.findIndex((c) => c.id == id);
  if (index > -1) {
    state.sliders.splice(index, 1);
  }
}
