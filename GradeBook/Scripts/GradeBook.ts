window.onload = function () {
    var grades = document.getElementsByClassName('grade');
    for (var i = 0; i < grades.length; i++) {
        var grade = grades[i] as HTMLInputElement;
        grade.onfocus = function () {
            this.style.border = '1px solid gray';
        };
        grade.onblur = function () {
            this.style.border = 'none';
        };
    }
}

function addAssignment(classID: number) {
    var title = (document.getElementById('new-assignment-title') as HTMLInputElement).value;
    var date = (document.getElementById('new-assignment-date') as HTMLInputElement).value;
    $.post('/gradebook/addassignment', {
        "title": title,
        "date": date,
        "classID": classID
    }, function () {
        location.reload();
    });
}

function updateScore(studentID: string, assignmentID: number, score: number, assessmentID: number) {
    if (assessmentID == null) {
        assessmentID = 0;
    }
    $.post('/GradeBook/UpdateScore', {
        "studentID": studentID,
        "assignmentID": assignmentID,
        "score": score,
        "assessmentID": assessmentID
    }, function () {
        location.reload();
        });
}

function updateAssignment(assignmentID: number, field: string, value: string) {
    $.post('/GradeBook/UpdateAssignment', {
        "assignmentID": assignmentID,
        "field": field,
        "value": value
    });
}

function sortGradeTable(column: number) {
    var table, rows, switching, i, x, y, shouldSwitch, dir, switchcount = 0;
    table = document.getElementById("GradeTable") as HTMLTableElement;
    switching = true;
    dir = "asc";
    while (switching) {
        switching = false;
        rows = table.getElementsByTagName("tr");
        for (i = 1; i < (rows.length - 1); i++) {
            shouldSwitch = false;
            var row = rows[i] as HTMLTableRowElement;

            if (column == 0) {
                x = rows[i].innerText;
                y = rows[i + 1].innerText;
            } else {
                x = (((((rows[i] as HTMLTableRowElement).cells[column] as HTMLTableDataCellElement)
                    .firstElementChild as HTMLDivElement)
                    .firstElementChild as HTMLDivElement)
                    .children[1] as HTMLDivElement).innerText;

                y = (((((rows[i + 1] as HTMLTableRowElement).cells[column] as HTMLTableDataCellElement)
                    .firstElementChild as HTMLDivElement)
                    .firstElementChild as HTMLDivElement)
                    .children[1] as HTMLDivElement).innerText;
            }
            if (dir == "asc") {
                if (x > y) {
                    shouldSwitch = true;
                    break;
                }
            } else if (dir == "desc") {
                if (x < y) {
                    shouldSwitch = true;
                    break;
                }
            }
        }
        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
            switchcount++;
        } else {
            if (switchcount == 0 && dir == "asc") {
                dir = "desc";
                switching = true;
            }
        }
    }
}

function toggleStudentGrades(studentID: string) {
    var row = document.getElementById('student-row-' + studentID) as HTMLTableRowElement;
    var collapsed = row.getElementsByClassName('collapse');
    $(collapsed).collapse('toggle');
}

function renameStudent(studentID: string, name: string) {
    $.post('gradebook/renamestudent', {
        "studentID": studentID,
        "name": name
    }, function () {
        location.reload();
    });
}