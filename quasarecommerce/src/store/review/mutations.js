export function setReviews (state,reviews) {
    state.reviews=reviews;
}

export function removeReview(state, id) {
    let index = state.reviews.findIndex((c) => c.id == id);
    if (index > -1) {
      state.reviews.splice(index, 1);
    }
  }

