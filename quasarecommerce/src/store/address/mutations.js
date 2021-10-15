export function setAddresses(state, addresses) {
  state.addresses = addresses;
}

export function addAddress(state, address) {
  state.addresses.push(address);
}

export function updateAddress(state, address) {
  let index = state.addresses.findIndex((c) => c.id == address.id);
  if (index > -1) {
    state.addresses.splice(index, 1, address);
  }
}

export function removeAddress(state, id) {
  let index = state.addresses.findIndex((c) => c.id == id);
  if (index > -1) {
    state.addresses.splice(index, 1);
  }
}
