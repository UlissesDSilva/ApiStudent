import AddressViewModel from "./address.model";
import GenderViewModel from "./gender.model";

export default interface StudentViewModel {
  id: string,
  firstName: string,
  lastName: string,
  dateOfBirth: Date,
  email: string,
  mobile: number,
  profileImageUrl?: string,
  gender: GenderViewModel,
  address: AddressViewModel
}
