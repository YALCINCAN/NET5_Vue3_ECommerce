export function getSliders (state) {
   return state.sliders;
}

export function getActiveSliders(state){
   return state.sliders.filter(slider=>slider.active)
}
