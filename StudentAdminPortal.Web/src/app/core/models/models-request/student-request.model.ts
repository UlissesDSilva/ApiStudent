export interface StudentUpdateRequestModel {
  firstName: string,
  lastName: string,
  dateOfBirth: Date,
  email: string,
  mobile: number,
  genderId: string,
  profileImageUrl?: string,
  physicalAddress: string,
  postalAddress: string,
}

export interface AddStudentRequestModel {
  id: string,
  firstName: string,
  lastName: string,
  dateOfBirth: Date,
  email: string,
  mobile: number,
  genderId: string,
  addressId: string,
  profileImageUrl?: string,
  physicalAddress: string,
  postalAddress: string,
}
