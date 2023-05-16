import { Component, ViewChild } from '@angular/core';
import { ThemePalette } from '@angular/material/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AppStrings } from 'src/app/core/constants/appString';
import GenderViewModel from 'src/app/core/models/gender.model';
import StudentViewModel from 'src/app/core/models/student.model';
import { GenderService } from 'src/app/core/services/gender/gender.service';
import { StudentService } from 'src/app/core/services/student/student.service';
import { studentObject } from 'src/app/core/models/objects/student-object';
import { StudentUpdateRequestModel, AddStudentRequestModel } from 'src/app/core/models/models-request/student-request.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { v4 as uuidv4 } from 'uuid';
import { finalize } from 'rxjs';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.scss']
})
export class StudentComponent {
  studentId?: string | null;
  colorLabelInput: ThemePalette = "accent"
  genders: GenderViewModel[] = [];
  labelHeader: string = AppStrings.student;
  student: StudentViewModel = studentObject;
  isLoadingResult: boolean = true;
  titleHeader: string = '';
  profileImage: string = '';
  file?: File;

  @ViewChild("studentDetailsForm") studentDetailsForm?: NgForm

  constructor(
    private genderService: GenderService,
    private studentService: StudentService,
    private _tootip: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router
  ) {
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.studentId = params['id'];
    })

    this.getAllGender();

    if(this.studentId) {
      this.getStudent(this.studentId);
    } else {
      this.setImage();
    }

    this.studentId == null ? this.titleHeader = "New Student" : this.titleHeader = "Edit Student"
  }

  getAllGender(): void {
    this.genderService.getAllGenders<GenderViewModel[]>().subscribe({
      next: next => {this.genders = next},
      complete: () => {this.isLoadingResult = false}
    });
  }

  getStudent(studentId: string): void {
    this.studentService.getStudentById<StudentViewModel>(studentId).subscribe({
      next: next=> {
        this.student = next;
      },
      complete: () => { this.setImage() }
    });
  }

  updateStudent(): void {
    const updateRequest: StudentUpdateRequestModel = {
      firstName: this.student.firstName,
      lastName: this.student.lastName,
      dateOfBirth: this.student.dateOfBirth,
      email: this.student.email,
      mobile: this.student.mobile,
      genderId: this.student.genderId,
      profileImageUrl: this.student.profileImageUrl,
      physicalAddress: this.student.address.physicalAddress,
      postalAddress: this.student.address.postalAddress
    }

    this.isLoadingResult = true;

    this.studentService.editStudent<StudentViewModel, StudentUpdateRequestModel>(this.student.id, updateRequest)
    .pipe(
      finalize(() => {
        if(this.file != null) {
          this.uploadImage(this.student.id, this.file);
        }
      })
    )
    .subscribe({
      next: next => {
        this.student = next;
        this._tootip.open("Success in update", undefined, {
          duration: 4000,
          direction: 'ltr',
          verticalPosition: 'top',
          panelClass: 'success-snackbar'
        });
      },
      error: error => {
        this._tootip.open(`Make sure the fields are filled in`, undefined, {
          duration: 4000,
          direction: 'ltr',
          verticalPosition: 'top',
          panelClass: 'error-snackbar'
        });
      },
      complete: () => {
        this.isLoadingResult = false;
      }
    })
  }

  createStudent(): void {
    const addRequest : AddStudentRequestModel = {
      id: uuidv4(),
      firstName: this.student.firstName,
      lastName: this.student.lastName,
      dateOfBirth: this.student.dateOfBirth,
      email: this.student.email,
      mobile: this.student.mobile,
      genderId: this.student.genderId,
      addressId: uuidv4(),
      profileImageUrl: this.student.profileImageUrl,
      physicalAddress: this.student.address.physicalAddress,
      postalAddress: this.student.address.postalAddress
    }

    if (this.studentDetailsForm?.valid) {
      this.isLoadingResult = true;

      this.studentService.createStudent<StudentViewModel, AddStudentRequestModel>(addRequest)
      .pipe(
        finalize(() => {
          // if(this.file != null) {
          //   this.uploadImage(this.student.id, this.file);
          // }
        })
      )
      .subscribe({
        next: next => {
          this.student = next;
          this._tootip.open(`Success in register student`, undefined, {
            duration: 2000,
            direction: 'ltr',
            verticalPosition: 'top',
            panelClass: 'success-snackbar'
          });
        },
        error: (error: any) => {
            this._tootip.open(`Error in register student`, undefined, {
              duration: 4000,
              direction: 'ltr',
              verticalPosition: 'top',
              panelClass: 'error-snackbar'
            });

          this.isLoadingResult = false;
        },
        complete: () => {
          this.isLoadingResult = false;
          if(this.file != null) {
            this.uploadImage(this.student.id, this.file);
          }
          this.router.navigateByUrl(`student/${this.student.id}`);
        }
      });
    }
  }

  deleteStudent(): void {
    this.isLoadingResult = true;

    this.studentService.deleteStudent(this.student.id).subscribe({
      next: () => {
        this._tootip.open(`Success in delete student`, undefined, {
          duration: 4000,
          direction: 'ltr',
          verticalPosition: 'top',
          panelClass: 'success-snackbar'
        });
      },
      error: () => {
        this._tootip.open(`Failed to delete a student`, undefined, {
          duration: 4000,
          direction: 'ltr',
          verticalPosition: 'top',
          panelClass: 'error-snackbar'
        });
      },
      complete: () => {
        this.isLoadingResult = false;
        this.router.navigate(['/'])
      }
    })
  }

  setImage(): void {
    if (this.student.profileImageUrl) {
      this.profileImage = this.studentService.getImagePath(this.student.profileImageUrl)
    } else {
      this.profileImage = '/assets/default-image.png'
    }
  }

  selectedImage(event: any): void {
    this.file = <File>event.target.files[0];
    const reader = new FileReader();
    reader.addEventListener('load', (evt: any) => {
      this.profileImage = evt.target.result
    })
    reader.readAsDataURL(event.target.files[0]);
  }

  uploadImage(studentId: string, file: any): void {
    if (studentId) {
      this.studentService.uploadImageUrl(studentId, file).subscribe({
        next: next => {
          this.student.profileImageUrl = next;
          this.setImage();
        }
      })
    }
  }
}
