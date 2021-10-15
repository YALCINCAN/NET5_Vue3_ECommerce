export function login(state, data) {
  state.loggedIn = true;
  localStorage.setItem("token", data.accessToken);
  localStorage.setItem("refreshtoken", data.refreshToken);
}

export function logout(state){
  state.loggedIn=false;
  localStorage.removeItem("token");
  localStorage.removeItem("refreshtoken");
}

export function setAuthenticatedUserWithRoles(state,data){
  state.loggedIn=true;
  state.authenticateduser.user=data.user;
  state.authenticateduser.roles=data.roles;
}


export function updateProfile(state,data){
  state.authenticateduser.user={...state.authenticateduser.user,...data};
}
