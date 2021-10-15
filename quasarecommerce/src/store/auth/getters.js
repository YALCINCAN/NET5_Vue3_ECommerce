export function getAuthenticatedUser(state) {
  return state.authenticateduser.user;
}

export function getLoggedIn(state) {
  return state.loggedIn;
}

export function getAuthenticatedUserRoles(state) {
  return state.authenticateduser.roles;
}

export function getFirstNameandLastName(state) {
  return (
    state.authenticateduser.user.firstName +
    " " +
    state.authenticateduser.user.lastName
  );
}
