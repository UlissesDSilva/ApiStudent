import { Component, Input } from '@angular/core';
import { ThemePalette } from '@angular/material/core';
import { ActivatedRoute } from '@angular/router';
import { AppStrings } from 'src/app/core/constants/appString';
import GenderViewModel from 'src/app/core/models/gender.model';
import StudentViewModel from 'src/app/core/models/student.model';
import { GenderService } from 'src/app/core/services/gender/gender.service';
import { StudentService } from 'src/app/core/services/student/student.service';

@Component({
  selector: 'student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.scss']
})
export class StudentComponent {
  studentId?: string | null;
  colorLabelInput: ThemePalette = "accent"
  genders: GenderViewModel[] = [];
  selectedValue: string = "";
  labelHeader: string = AppStrings.student;

  student?: StudentViewModel;

  constructor(
    private genderService: GenderService,
    private studentService: StudentService,
    private route: ActivatedRoute
  ) {
    this.route.params.subscribe(params => {
      this.studentId = params['id'];
    })
  }

  ngOnInit(): void {
    this.getAllGender();
    if(this.studentId) {
      this.getStudent(this.studentId);
    }
  }

  getAllGender() {
    this.genderService.getAllGenders<GenderViewModel[]>().subscribe({
      next: next => {this.genders = next}
    })
  }

  getStudent(studentId: string) {
    this.studentService.getStudentById<StudentViewModel>(studentId).subscribe({
      next: next=> {
        this.student = next;
        this.selectedValue = this.student.gender.description;
      }
    });
  }
}
