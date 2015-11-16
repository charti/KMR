#bump AssemblyVersion
require 'rake'

task :bump_version => :restore_assembly do |t|
	puts "bumping version"

	assemblies = FileList.new("**/AssemblyInfo.cs").each do |path|
			new_assembly = ""

	File.read(path).each_line do |line|
		version_type = /(AssemblyVersion|AssemblyFileVersion)/.match(line)
		if (version_type.nil? || line.include?('//'))
			new_assembly << line
		else
			version = /([\d+\*])\.([\d+\*])\.([\d+\*])/.match(line)

			new_version = /([\d+\*])\.([\d+\*])\.([\d+\*])/.match(ENV['VERSION'])

			major = new_version[1].eql?('*') ? '0' : version[1].to_i + new_version[1].to_i
			minor = new_version[2].eql?('*') ? '0' : version[2].to_i + new_version[2].to_i
			build = new_version[3].eql?('*') ? '0' : version[3].to_i + new_version[3].to_i

			file = File.new("postbuild.prop", "w")
			file.write("VERSION=#{major}.#{minor}.#{build}")
			file.flush
			file.close

			puts "new Version: #{major}.#{minor}.#{build}"

			new_assembly << line.gsub(/(?<=")(.*)(?=")/, "#{major}.#{minor}.#{build}")
		end
	end
	File.write(path, new_assembly)
	end
end

task :restore_assembly do |t|
	restored = File.expand_path("#{ENV["RESTORE_ASSEMBLY"]}\\Properties\\AssemblyInfo.cs")
	destination = File.expand_path("#{ENV["WORKSPACE"]}\\KMR\\Properties\\AssemblyInfo.cs")
	next unless File.exist?(restored)
	FileUtils.cp(restored, destination)
end

Rake.application[:bump_version].invoke