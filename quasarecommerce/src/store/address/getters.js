export function getAddresses(state) {
  return state.addresses;
}

export function getAddressById(state){
  return id => state.addresses.find(x=>x.id===id)
}