class Test_Clause_1:
    def import_dictionary(self, d):
        return d
    def assign_variables(self, d1,d2):

        variable_1 =d1
        variable_2 = d2
        new_dict = "{TestVariable1:" + str(variable_1) + ',"TestVariable2":' + str(variable_2) + '}'
        return new_dict